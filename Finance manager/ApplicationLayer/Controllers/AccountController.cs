using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using ApplicationLayer.Security.Jwt;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationLayer.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly ITokenManager _tokenManager;

    public AccountController(IAccountService service, IMapper mapper, ITokenManager tokenManager, ILogger<AccountController> logger) : base(mapper, logger)
    {
        _accountService = service ?? throw new ArgumentNullException(nameof(service));
        _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LogInAsync(string email, string password)
    {
        _logger.LogInformation("LogInAsync called with email: {Email}", email);

        var identity = await _tokenManager.GetIdentityAsync(email, password);
        if (identity == null)
        {
            _logger.LogWarning("Login failed for email: {Email}. Invalid credentials.", email);
            return BadRequest(new { errorText = "Invalid email or username" });
        }

        var encodedJwt = _tokenManager.CreateToken(identity);

        _logger.LogInformation("Login successful for email: {Email}", email);

        var response = new
        {
            access_token = encodedJwt,
            email = identity.Name
        };

        return new JsonResult(response);
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<ActionResult<AccountDTO>> CreateAsync([FromBody] AccountDTO account)
    {
        _logger.LogInformation("CreateAsync called to create a new account with email: {Email}", account.Email);

        var newAccount = _mapper.Map<AccountDTO>(
                       await _accountService.AddAccountAsync(
                           _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Account created successfully with email: {Email} and Id: {Id}", newAccount.Email, newAccount.Id);

        return newAccount;
    }

    [HttpPut("update")]
    public async Task<AccountDTO> UpdateAsync([FromBody] AccountDTO account)
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

        return updatedAccount;
    }

    [HttpDelete("remove")]
    public void Delete()
    {
        int id = GetUserId();
        _logger.LogInformation("Delete called to remove account with Id: {Id}", id);

        _accountService.DeleteAccountWithId(id);

        _logger.LogInformation("Account with Id: {Id} deleted successfully", id);
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpGet("accounts/admin")]
    public async Task<List<AccountDTO>> GetAllAsync(int skip, int take)
    {
        _logger.LogInformation("GetAllAsync called by admin with skip: {Skip}, take: {Take}", skip, take);

        var accounts = (await _accountService.GetAccountsAsync(GetUserEmail(), skip, take))
                .Select(_mapper.Map<AccountDTO>)
                .ToList();

        _logger.LogInformation("{Count} accounts retrieved successfully", accounts.Count);

        return accounts;
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpPut("update/admin")]
    public async Task<AccountDTO> AdminUpdateAccountAsync([FromBody] AccountDTO account)
    {
        _logger.LogInformation("AdminUpdateAccountAsync called to update account with Id: {Id}", account.Id);

        var updatedAccount = _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Admin updated account with Id: {Id} successfully", updatedAccount.Id);

        return updatedAccount;
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("remove/{id}/admin")]
    public void DeleteById(int id)
    {
        _logger.LogInformation("DeleteById called by admin to remove account with Id: {Id}", id);

        _accountService.DeleteAccountWithId(id);

        _logger.LogInformation("Admin deleted account with Id: {Id} successfully", id);
    }

}
