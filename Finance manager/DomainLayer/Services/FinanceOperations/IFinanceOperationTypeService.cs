using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
