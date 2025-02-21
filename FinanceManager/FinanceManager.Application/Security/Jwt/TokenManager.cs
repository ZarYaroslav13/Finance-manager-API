using FinanceManager.Application.Models;
using AutoMapper;
using FinanceManager.Domain.Services.Accounts;
using FinanceManager.Domain.Services.Admins;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinanceManager.Application.Security.Jwt;

public class TokenManager : ITokenManager
{
    private readonly IAccountService _accountService;
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;
    private readonly AuthOptions _authOptions;

    public TokenManager(IAccountService accountService, IAdminService adminService, IMapper mapper, IOptions<AuthOptions> authOptions)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _adminService = adminService ?? throw new ArgumentNullException(nameof(adminService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        ArgumentNullException.ThrowIfNull(authOptions);
        _authOptions = authOptions.Value ?? throw new ArgumentNullException(nameof(authOptions.Value));
    }

    public string CreateToken(ClaimsIdentity identity)
    {
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
                issuer: _authOptions.ISSUER,
                audience: _authOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authOptions.LIFETIME_IN_MINETS)),
                signingCredentials: new(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<ClaimsIdentity> GetAccountIdentityAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(email) + "or" + nameof(password));

        AccountDTO account = _mapper.Map<AccountDTO>(await _accountService.TrySignInAsync(email, password));

        if (account == null)
            return null;

        var claims = new List<Claim>()
        {
            new(nameof(AccountDTO.Id), account.Id.ToString()),
            new(ClaimsIdentity.DefaultNameClaimType, account.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, _accountService.GetNameAccountRole())
        };

        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        return identity;
    }

    public async Task<ClaimsIdentity> GetAdminIdentityAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(email) + "or" + nameof(password));

        AdminDTO account = _mapper.Map<AdminDTO>(await _adminService.TrySignInAsync(email, password));

        if (account == null)
            return null;

        var claims = new List<Claim>()
        {
            new(nameof(AdminDTO.Id), account.Id.ToString()),
            new(ClaimsIdentity.DefaultNameClaimType, account.Email),
            new(ClaimsIdentity.DefaultRoleClaimType, _adminService.GetNameAdminRole())
        };

        ClaimsIdentity identity = new(claims, "Token",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        return identity;
    }
}
