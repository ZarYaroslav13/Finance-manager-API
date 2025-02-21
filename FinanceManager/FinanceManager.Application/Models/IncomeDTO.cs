using Infrastructure.Models;

namespace FinanceManager.Application.Models;

public class IncomeDTO : FinanceOperationDTO
{
    public IncomeDTO()
    {

    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Income)
            throw new ArgumentException(nameof(type));
    }
}