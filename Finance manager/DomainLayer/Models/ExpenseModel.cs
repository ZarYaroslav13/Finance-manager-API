using DataLayer.Models;

namespace DomainLayer.Models;

public class ExpenseModel : FinanceOperationModel
{
    public ExpenseModel(FinanceOperationTypeModel type) : base(type)
    {
        if (type.EntryType != DataLayer.Models.EntryType.Expense)
            throw new ArgumentException(nameof(type));
    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeModel type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Expense)
            throw new ArgumentException(nameof(type));
    }
}
