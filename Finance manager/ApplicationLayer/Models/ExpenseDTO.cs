using DataLayer.Models;

namespace ApplicationLayer.Models;

public class ExpenseDTO : FinanceOperationDTO
{
    public ExpenseDTO(FinanceOperationTypeDTO type) : base(type)
    {
    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Expense)
            throw new ArgumentException(nameof(type));
    }
}
