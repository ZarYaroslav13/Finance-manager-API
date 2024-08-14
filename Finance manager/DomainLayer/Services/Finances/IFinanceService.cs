using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public interface IFinanceService
{
    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesOfWallet(int walletId);

    public FinanceOperationTypeModel AddFinanceOperationType(FinanceOperationTypeModel type);

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type);

    public void DeleteFinanceOperationType(int id);

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId);

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId, int numberOfOperation);

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId, DateTime startDate, DateTime endDate);

    public List<FinanceOperationModel> GetAllFinanceOperationOfType(int TypeId);

    public FinanceOperationModel AddFinanceOperationType(FinanceOperationModel financeOperation);

    public FinanceOperationModel UpdateFinanceOperationType(FinanceOperationModel financeOperation);

    public void DeleteFinanceOperation(int id);
}
