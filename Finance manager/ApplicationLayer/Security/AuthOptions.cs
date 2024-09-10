using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApplicationLayer.Security;

public class AuthOptions
{
    public const string ISSUER = "https://localhost:7099";
    public const string AUDIENCE = "https://localhost:7099";
    const string KEY = "mysupersecret_secretkey!123";
    public const int LIFETIME_IN_MINETS = 60;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF32.GetBytes(KEY));
    }
}
