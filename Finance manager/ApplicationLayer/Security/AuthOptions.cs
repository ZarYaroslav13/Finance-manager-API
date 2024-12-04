using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApplicationLayer.Security;

public class AuthOptions
{
    public const string Auth = "Auth";

    public string ISSUER { get; set; } = "https://localhost:7099";
    public string AUDIENCE { get; set; } = "https://localhost:7099";
    public string KEY { get; set; } = "mysupersecret_secretkey!123";
    public int LIFETIME_IN_MINETS { get; set; } = 60;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF32.GetBytes(KEY));
    }
}
