using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Services.Wallets;

public interface IWalletService
{
    public Task<List<WalletModel>> GetAllWalletsOfAccountAsync(int accountId);

    public Task<WalletModel> AddWalletAsync(WalletModel wallet);

    public Task<WalletModel> UpdateWalletAsync(WalletModel updatedWallet);

    public Task DeleteWalletByIdAsync(int id);

    public Task<WalletModel> FindWalletAsync(int id);

    public Task<bool> IsAccountOwnerWalletAsync(int acoountId, int walletId);
}
