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
        var identity = await _tokenManager.GetIdentityAsync(email, password);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid email or username" });
        }

        var encodedJwt = _tokenManager.CreateToken(identity);

        var response = new
        {
            access_token = encodedJwt,
            email = identity.Name
        };
        _logger.LogInformation("Mr with email {@Email} logged in ", identity.Name);

        return new JsonResult(response);
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<ActionResult<AccountDTO>> CreateAsync([FromBody] AccountDTO account)
    {
        var newAccount = _mapper.Map<AccountDTO>(
                   await _accountService.AddAccountAsync(
                       _mapper.Map<AccountModel>(account)));

        return newAccount;
    }

    [HttpPut("update")]
    public async Task<AccountDTO> UpdateAsync([FromBody] AccountDTO account)
    {
        int id = GetUserId();

        if (account.Id != id)
            throw new UnauthorizedAccessException(nameof(account));

        return _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));
    }

    [HttpDelete("remove")]
    public void Delete()
    {
        _accountService.DeleteAccountWithId(GetUserId());
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpGet("accounts/admin")]
    public async Task<List<AccountDTO>> GetAllAsync(int skip, int take)
    {
        return (await _accountService.GetAccountsAsync(GetUserEmail(), skip, take))
                .Select(_mapper.Map<AccountDTO>)
                .ToList();
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpPut("update/admin")]
    public async Task<AccountDTO> AdminUpdateAccountAsync([FromBody] AccountDTO account)
    {
        return _mapper.Map<AccountDTO>(
                await _accountService.UpdateAccountAsync(
                    _mapper.Map<AccountModel>(account)));
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("remove/{id}/admin")]
    public void DeleteById(int id)
    {
        _accountService.DeleteAccountWithId(id);
    }
}
