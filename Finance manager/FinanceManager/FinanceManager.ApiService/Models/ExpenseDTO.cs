using Infrastructure.Models;

namespace FinanceManager.ApiService.Models;

public class ExpenseDTO : FinanceOperationDTO
{
    public ExpenseDTO()
    {
    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Expense)
            throw new ArgumentException(nameof(type));
    }
}
