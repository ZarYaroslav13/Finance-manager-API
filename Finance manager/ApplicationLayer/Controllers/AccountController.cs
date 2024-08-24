using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using ApplicationLayer.Security.Jwt;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApplicationLayer.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly ITokenManager _tokenManager;
    private const string _adminPolicy = "OnlyForAdmins";

    public AccountController(IAccountService service, IMapper mapper, ITokenManager tokenManager, ILogger<BaseController> logger) : base(mapper, logger)
    {
        _accountService = service ?? throw new ArgumentNullException(nameof(service));
        _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult LogIn(string email, string password)
    {
        var identity = GetIdentity(email, password);

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

        return new JsonResult(response);
    }

    [HttpPost("CreateAccount")]
    [AllowAnonymous]
    public ActionResult<AccountDTO> CreateAccount(AccountDTO account)
    {
        var newAccount = _mapper.Map<AccountDTO>(
                   _accountService.AddAccount(
                       _mapper.Map<AccountModel>(account)));

        return newAccount;
    }

    [HttpPut("UpdateAccount")]
    public AccountDTO UpdateAccount(AccountDTO account)
    {
        int id = GetUserId();

        if (account.Id != id)
            throw new UnauthorizedAccessException(nameof(account));

        return _mapper.Map<AccountDTO>(
                _accountService.UpdateAccount(
                    _mapper.Map<AccountModel>(account)));
    }

    [HttpDelete("DeleteAccount")]
    public void DeleteAccountById()
    {
        _accountService.DeleteAccountWithId(GetUserId());
    }

    #region Endpoints for admins
    [Authorize(Policy = _adminPolicy)]
    [HttpGet("Admin/GetAccounts")]
    public List<AccountDTO> GetAllAccounts(int skip, int take)
    {
        return _accountService.GetAccounts(skip, take)
                .Select(_mapper.Map<AccountDTO>)
                .ToList();
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpPut("Admin/UpdateAccount")]
    public AccountDTO AdminUpdateAccount(AccountDTO account)
    {
        return _mapper.Map<AccountDTO>(
                _accountService.UpdateAccount(
                    _mapper.Map<AccountModel>(account)));
    }

    [Authorize(Policy = _adminPolicy)]
    [HttpDelete("Admin/DeleteAccount/{id}")]
    public void DeleteAccountById(int id)
    {
        _accountService.DeleteAccountWithId(id);
    }
    #endregion

    private ClaimsIdentity GetIdentity(string email, string password)
    {
        AccountDTO account = _mapper.Map<AccountDTO>(_accountService.TryLogIn(email, password));

        if (account == null)
            return null;

        var claims = new List<Claim>()
        {
            new(nameof(AccountDTO.Id), account.Id.ToString()),
            new(ClaimsIdentity.DefaultNameClaimType, account.Email),
        };

        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        return identity;
    }
}
