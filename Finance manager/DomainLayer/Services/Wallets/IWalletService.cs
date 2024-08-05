using DataLayer.Models;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services.Wallets;

public interface IWalletService
{
    public List<WalletModel> GetAllWalletsOfAccount(int accountId);

    public WalletModel AddNewWallet(Wallet wallet);

    public WalletModel Update(Wallet updatedWallet);

    public void Delete(int id);

    public WalletModel Find(int id);
}
