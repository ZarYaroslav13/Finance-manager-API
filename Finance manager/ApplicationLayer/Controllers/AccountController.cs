using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService service, IMapper mapper, ILogger<AccountController> logger) : base(mapper, logger)
    {
        _accountService = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Policy = AdminService.NameAdminPolicy)]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int skip, int take)
    {
        _logger.LogInformation("GetAllAsync called by admin with skip: {Skip}, take: {Take}", skip, take);

        var accounts = (await _accountService.GetAccountsAsync(GetUserEmail(), skip, take))
                .Select(_mapper.Map<AccountDTO>)
                .ToList();

        _logger.LogInformation("{Count} accounts retrieved successfully", accounts.Count);

        return Ok(accounts);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] AccountDTO account)
    {
        int userId = GetUserId();
        string userRole = GetUserRole();

        _logger.LogInformation("UpdateAsync called to update account with Id: {Id} by user with id: {UserId} and role {UserRole}", account.Id, userId, userRole);

        if (userRole != AdminService.NameAdminRole && account.Id != userId)
        {
            _logger.LogWarning($"Unauthorized access attempt to update account with Id: {account.Id} by user with id: {userId} and role {userRole}");
            throw new UnauthorizedAccessException($"Access denied");
        }

        var updatedAccount = _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Account with id: {Id} updated successfully by user with id: {UserId} and role {UserRole}", updatedAccount.Id, userId, userRole);

        return Ok(updatedAccount);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUserById(int id)
    {
        int userId = GetUserId();
        string userRole = GetUserRole();

        _logger.LogInformation("DeleteById called by admin to remove account with Id: {Id}", id);

        if (userRole != AdminService.NameAdminRole && id != userId)
        {
            _logger.LogWarning($"Unauthorized access attempt to update account with Id: {id} by user with id: {userId} and role {userRole}");
            throw new UnauthorizedAccessException($"Access denied");
        }

        _accountService.DeleteAccountWithId(id);

        _logger.LogInformation("Admin deleted account with Id: {Id} successfully", id);

        return Ok();
    }
}
