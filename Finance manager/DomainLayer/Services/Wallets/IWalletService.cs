using DomainLayer.Models;

namespace DomainLayer.Services.Wallets;

public interface IWalletService
{
    public List<WalletModel> GetAllWalletsOfAccount(int accountId);

    public WalletModel AddWallet(WalletModel wallet);

    public WalletModel UpdateWallet(WalletModel updatedWallet);

    public void DeleteWalletById(int id);

    public WalletModel FindWallet(int id);

    public bool IsAccountOwnerWallet(int acoountId, int walletId);
}
