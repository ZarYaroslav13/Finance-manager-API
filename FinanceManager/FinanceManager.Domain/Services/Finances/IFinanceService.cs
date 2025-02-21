using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Services.Finances;

public interface IFinanceService
{
    public Task<bool> IsAccountOwnerOfWalletAsync(int accountid, int walletId);

    public Task<List<FinanceOperationTypeModel>> GetAllFinanceOperationTypesOfWalletAsync(int walletId);

    public Task<FinanceOperationTypeModel> AddFinanceOperationTypeAsync(FinanceOperationTypeModel type);

    public Task<FinanceOperationTypeModel> UpdateFinanceOperationTypeAsync(FinanceOperationTypeModel type);

    public Task DeleteFinanceOperationTypeAsync(int id);

    public Task<bool> IsAccountOwnerOfFinanceOperationTypeAsync(int accountid, int typeId);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, int index = 0, int count = 0);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, DateTime startDate, DateTime endDate);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfTypeAsync(int TypeId);

    public Task<FinanceOperationModel> AddFinanceOperationAsync(FinanceOperationModel financeOperation);

    public Task<FinanceOperationModel> UpdateFinanceOperationAsync(FinanceOperationModel financeOperation);

    public Task DeleteFinanceOperationAsync(int id);

    public Task<bool> IsAccountOwnerOfFinanceOperationAsync(int accountid, int typeId);
}
