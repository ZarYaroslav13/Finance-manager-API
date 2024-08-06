using DomainLayer.Models;

namespace DomainLayer.Services.FinanceOperations;

public interface IFinanceOperationTypeService
{
    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesWithWalletId(int walletId);

    public FinanceOperationTypeModel AddNewFinanceOperationType(FinanceOperationTypeModel type);

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type);

    public void DeleteFinanceOperationType(int id);

    public List<FinanceOperationModel> GetAllFinanceOperationWithTypeId(int TypeId);

    public FinanceOperationModel AddNewFinanceOperationType(FinanceOperationModel financeOperation);

    public FinanceOperationModel UpdateFinanceOperationType(FinanceOperationModel financeOperation);

    public void DeleteFinanceOperation(int id);
}
