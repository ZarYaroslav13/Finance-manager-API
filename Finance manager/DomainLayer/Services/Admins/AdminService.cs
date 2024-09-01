using DataLayer;
using DataLayer.Models;

namespace DomainLayer.Services.Admins;

public class AdminService : IAdminService
{
    private const int AdminNumberInArray = 5;
    public List<Account> Admins { get; } = new()
    {
        FillerBbData.Accounts[AdminNumberInArray]
    };

    public bool IsItAdmin(string email)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email);

        return Admins.Any(a => a.Email == email);
    }
}
