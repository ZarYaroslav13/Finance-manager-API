using Infrastructure.Models;

namespace DomainLayer.Models;

public class IncomeModel : FinanceOperationModel
{
    public IncomeModel(FinanceOperationTypeModel type) : base(type)
    {

    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeModel type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Income)
            throw new ArgumentException(nameof(type));
    }
}
