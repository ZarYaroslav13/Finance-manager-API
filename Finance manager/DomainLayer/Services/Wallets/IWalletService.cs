using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayer.Services.Wallets;

public interface IWalletService
{
    public List<WalletModel> GetAllWalletsOfAccount(int accountId);

    public WalletModel AddNewWallet(Wallet wallet);

    public WalletModel Update(Wallet updatedWallet);

    public void Delete(int id);

    public WalletModel Find(int id);
}
