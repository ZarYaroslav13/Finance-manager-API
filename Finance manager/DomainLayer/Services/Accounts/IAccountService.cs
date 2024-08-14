using DomainLayer.Models;

namespace DomainLayer.Services.Accounts;

public interface IAccountService
{
    public AccountModel AddAccount(AccountModel account);

    public AccountModel UpdateAccount(AccountModel updatedAccount);

    public void DeleteAccountWithId(int id);

    public AccountModel TryLogIn(string email, string password);

    public bool IsItEmail(string emailAddress);

    public bool CanTakeThisEmail(string emailAddress);
}
