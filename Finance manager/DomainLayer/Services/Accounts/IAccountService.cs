using DataLayer.Models;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Accounts;

public interface IAccountService
{
    public AccountModel AddNewAccount(Account account);

    public AccountModel UpdateAccount(Account updatedAccount);

    public void DeleteAccountWithId(int id);

    public AccountModel TryLogIn(string email, string password);
}
