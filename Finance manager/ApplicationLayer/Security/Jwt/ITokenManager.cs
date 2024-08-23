using System.Security.Claims;

namespace ApplicationLayer.Security.Jwt;

public interface ITokenManager
{
    public string CreateToken(ClaimsIdentity identity);
}
