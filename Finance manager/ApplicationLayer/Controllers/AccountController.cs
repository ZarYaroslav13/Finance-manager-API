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

    [Authorize(Policy = AdminService.NameAdminPolicy)]
    [HttpPut("{id}")]
    public async Task<IActionResult> AdminUpdateAccountAsync([FromBody] AccountDTO account)
    {
        _logger.LogInformation("AdminUpdateAccountAsync called to update account with Id: {Id}", account.Id);

        var updatedAccount = _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Admin updated account with Id: {Id} successfully", updatedAccount.Id);

        return Ok(updatedAccount);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] AccountDTO account)
    {
        int id = GetUserId();
        _logger.LogInformation("UpdateAsync called to update account with Id: {Id}", id);

        if (account.Id != id)
        {
            throw new UnauthorizedAccessException($"Unauthorized access attempt to update account with Id: {account.Id}");
        }

        var updatedAccount = _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Account with Id: {Id} updated successfully", updatedAccount.Id);

        return Ok(updatedAccount);
    }

    [Authorize(Policy = AdminService.NameAdminPolicy)]
    [HttpDelete("{id}")]
    public IActionResult DeleteUserById(int id)
    {
        _logger.LogInformation("DeleteById called by admin to remove account with Id: {Id}", id);

        _accountService.DeleteAccountWithId(id);

        _logger.LogInformation("Admin deleted account with Id: {Id} successfully", id);

        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        int id = GetUserId();
        _logger.LogInformation("Delete called to remove account with Id: {Id}", id);

        _accountService.DeleteAccountWithId(id);

        _logger.LogInformation("Account with Id: {Id} deleted successfully", id);

        return Ok();
    }
}
