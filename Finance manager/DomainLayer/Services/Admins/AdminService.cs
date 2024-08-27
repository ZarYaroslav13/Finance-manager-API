using DataLayer;
using DataLayer.Models;

namespace DomainLayer.Services.Admins;

public class AdminService : IAdminService
{
    private const int AdminNumberInArray = 5;
    private readonly List<Account> Admins = new()
    {
        FillerBbData.Accounts[AdminNumberInArray]
    };

    public bool IsItAdmin(string email)
    {
        return Admins.Any(a => a.Email == email);
    }
}
