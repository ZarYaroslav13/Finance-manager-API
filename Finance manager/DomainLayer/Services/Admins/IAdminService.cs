using DomainLayer.Models;

namespace DomainLayer.Services.Admins;

public interface IAdminService
{
    public string GetNameAdminRole();

    public string GetAdminPolicy();

    public List<AdminModel> GetAdmins();

    public Task<AdminModel> TrySignInAsync(string email, string password);

    public bool IsItAdmin(string email);
}
