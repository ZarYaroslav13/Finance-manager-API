using ApplicationLayer.Controllers.Base;
using ApplicationLayer.Models;
using ApplicationLayer.Security;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApplicationLayer.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController : EntityController
{
    IAccountService _accountService;

    public AccountController(ILogger<EntityController> logger, IMapper mapper, IAccountService service) : base(logger, mapper)
    {
        _accountService = service;
    }

    [HttpGet("LogIn")]
    [AllowAnonymous]
    public IActionResult LogIn(string email, string password)
    {
        var identity = GetIdentity(email, password);

        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid email or username" });
        }

        var now = DateTime.UtcNow;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.Aes128CbcHmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            email = identity.Name
        };

        return Json(response);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult CreateAccount(AccountDTO account)
    {
        try
        {
            var newAccount = _mapper.Map<AccountDTO>(
                    _accountService.AddAccount(
                        _mapper.Map<AccountModel>(account)));

            return LogIn(newAccount.Email, account.Password);
        }
        catch (Exception e)
        {
            return BadRequest(new { errorText = e.Message });
        }
    }

    [HttpPut]
    public AccountDTO UpdateAccount(AccountDTO account)
    {
        return _mapper.Map<AccountDTO>(
                _accountService.UpdateAccount(
                    _mapper.Map<AccountModel>(account)));
    }

    [HttpDelete]
    public void DeleteAccountById(int id)
    {
        _accountService.DeleteAccountWithId(id);
    }

    private ClaimsIdentity GetIdentity(string email, string password)
    {
        AccountDTO account = _mapper.Map<AccountDTO>(_accountService.TryLogIn(email, password));

        if (account == null)
            return null;

        var claims = new List<Claim>()
        {
            new("Id", account.Id.ToString()),
            new(ClaimsIdentity.DefaultNameClaimType, account.Email),
        };

        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        return identity;
    }
}
