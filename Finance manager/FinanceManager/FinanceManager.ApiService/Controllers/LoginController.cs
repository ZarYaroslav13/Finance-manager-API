using FinanceManager.ApiService.Controllers.Base;
using FinanceManager.ApiService.Models;
using FinanceManager.ApiService.Security.Jwt;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Services.Accounts;
using DomainLayer.Services.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.ApiService.Controllers;

[AllowAnonymous]
[Route("finance-manager/authorization")]
public class LoginController : BaseController
{
    private readonly IAccountService _accountService;
    private readonly IAdminService _adminService;
    private readonly ITokenManager _tokenManager;

    public LoginController(
        IAdminService adminService,
        IAccountService accountService,
        ITokenManager tokenManager,
        IMapper mapper,
        ILogger<LoginController> logger) : base(mapper, logger)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        _tokenManager = tokenManager ?? throw new ArgumentNullException(nameof(tokenManager));
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> SignInAsync(string email, string password)
    {
        _logger.LogInformation("SignInAsync called with email: {Email}", email);

        if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
        {
            _logger.LogWarning($"{nameof(email)}: {email} \n {nameof(password)}: {password}");
            return BadRequest(new { errorText = "Email and password cannot be empty" });
        }

        var identity = await _tokenManager.GetAccountIdentityAsync(email, password);
        if (identity == null)
        {
            _logger.LogWarning("Sign in failed for email: {Email}. Invalid credentials.", email);
            return BadRequest(new { errorText = "Invalid email or username" });
        }

        var encodedJwt = _tokenManager.CreateToken(identity);

        _logger.LogInformation("Sign in successful for email: {Email}", email);

        var response = new
        {
            access_token = encodedJwt,
            email = identity.Name,
            user_id = identity.FindFirst(nameof(AccountDTO.Id)).Value
        };

        return Ok(response);
    }

    [HttpPost("admin/login")]
    public async Task<IActionResult> SignInAdminAsync(string email, string password)
    {
        _logger.LogInformation("SignInAdminAsync called with email: {Email}", email);

        if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
        {
            _logger.LogWarning($"{nameof(email)}: {email} \n {nameof(password)}: {password}");
            return BadRequest(new { errorText = "Email and password cannot be empty" });
        }

        var identity = await _tokenManager.GetAdminIdentityAsync(email, password);
        if (identity == null)
        {
            _logger.LogWarning("SignInAdmin failed for email: {Email}. Invalid credentials.", email);
            return BadRequest(new { errorText = "Invalid email" });
        }

        var encodedJwt = _tokenManager.CreateToken(identity);

        _logger.LogInformation("SignInAdmin successful for email: {Email}", email);

        var response = new
        {
            access_token = encodedJwt,
            email = identity.Name
        };

        return Ok(response);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> CreateAsync([FromBody] AccountDTO account)
    {
        _logger.LogInformation("CreateAsync called to create a new account with email: {Email}", account.Email);

        var newAccount = _mapper.Map<AccountDTO>(
                       await _accountService.AddAccountAsync(
                           _mapper.Map<AccountModel>(account)));

        _logger.LogInformation("Account created successfully with email: {Email} and Id: {Id}", newAccount.Email, newAccount.Id);

        return Ok(newAccount);
    }
}
