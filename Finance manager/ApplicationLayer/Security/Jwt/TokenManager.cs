using ApplicationLayer.Models;
using AutoMapper;
using DomainLayer.Services.Accounts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApplicationLayer.Security.Jwt;

public class TokenManager : ITokenManager
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public TokenManager(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public string CreateToken(ClaimsIdentity identity)
    {
        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME_IN_MINETS)),
                signingCredentials: new(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(email) + "or" + nameof(password));

        AccountDTO account = _mapper.Map<AccountDTO>((await _accountService.TryLogInAsync(email, password)));

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
