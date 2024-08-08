using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public interface IFinanceService
{
    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesOfWallet(int walletId);

    public FinanceOperationTypeModel AddNewFinanceOperationType(FinanceOperationTypeModel type);

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type);

    public void DeleteFinanceOperationType(int id);

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId);

    public List<FinanceOperationModel> GetAllFinanceOperationOfType(int TypeId);

    public FinanceOperationModel AddNewFinanceOperationType(FinanceOperationModel financeOperation);

    public FinanceOperationModel UpdateFinanceOperationType(FinanceOperationModel financeOperation);

    public void DeleteFinanceOperation(int id);
}
