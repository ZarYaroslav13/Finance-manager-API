using System.Security.Claims;

namespace FinanceManager.ApiService.Security.Jwt;

public interface ITokenManager
{
    public string CreateToken(ClaimsIdentity identity);

    public Task<ClaimsIdentity> GetAccountIdentityAsync(string email, string password);

    public Task<ClaimsIdentity> GetAdminIdentityAsync(string email, string password);
}
