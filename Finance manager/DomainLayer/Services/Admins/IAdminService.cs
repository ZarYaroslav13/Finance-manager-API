using DataLayer.Models;

namespace DomainLayer.Services.Admins;

public interface IAdminService
{
    public List<Account> Admins { get; }

    public bool IsItAdmin(string email);
}
