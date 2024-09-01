using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public interface IFinanceService
{
    public Task<List<FinanceOperationTypeModel>> GetAllFinanceOperationTypesOfWalletAsync(int walletId);

    public Task<FinanceOperationTypeModel> AddFinanceOperationTypeAsync(FinanceOperationTypeModel type);

    public Task<FinanceOperationTypeModel> UpdateFinanceOperationTypeAsync(FinanceOperationTypeModel type);

    public Task DeleteFinanceOperationTypeAsync(int id);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, int index = 0, int count = 0);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, DateTime startDate, DateTime endDate);

    public Task<List<FinanceOperationModel>> GetAllFinanceOperationOfTypeAsync(int TypeId);

    public Task<FinanceOperationModel> AddFinanceOperationAsync(FinanceOperationModel financeOperation);

    public Task<FinanceOperationModel> UpdateFinanceOperationAsync(FinanceOperationModel financeOperation);

    public Task DeleteFinanceOperationAsync(int id);
}
