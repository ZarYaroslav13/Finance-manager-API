using Infrastructure.Models;

namespace FinanceManager.Domain.Models;

public class ExpenseModel : FinanceOperationModel
{
    public ExpenseModel(FinanceOperationTypeModel type) : base(type)
    {

    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeModel type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Expense)
            throw new ArgumentException(nameof(type));
    }
}
