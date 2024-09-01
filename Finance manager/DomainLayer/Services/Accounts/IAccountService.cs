using DomainLayer.Models;

namespace DomainLayer.Services.Accounts;

public interface IAccountService
{
    public Task<List<AccountModel>> GetAccountsAsync(string adminEmail, int skip = 0, int take = 0);

    public Task<AccountModel> AddAccountAsync(AccountModel account);

    public Task<AccountModel> UpdateAccountAsync(AccountModel updatedAccount);

    public void DeleteAccountWithId(int id);

    public Task<AccountModel> TryLogInAsync(string email, string password);

    public bool IsItEmail(string emailAddress);

    public Task<bool> CanTakeThisEmailAsync(string emailAddress);
}
