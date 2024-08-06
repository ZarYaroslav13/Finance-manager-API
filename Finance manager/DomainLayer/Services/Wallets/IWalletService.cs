using DataLayer.Models;
using DomainLayer.Models;

namespace DomainLayer.Services.Wallets;

public interface IWalletService
{
    public List<WalletModel> GetAllWalletsOfAccount(int accountId);

    public WalletModel AddNewWallet(WalletModel wallet);

    public WalletModel UpdateWallet(WalletModel updatedWallet);

    public void DeleteWalletById(int id);

    public WalletModel FindWallet(int id);
}
