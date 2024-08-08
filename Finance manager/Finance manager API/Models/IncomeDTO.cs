using DataLayer.Models;

namespace Finance_manager_API.Models;

public class IncomeDTO : FinanceOperationDTO
{
    public IncomeDTO(FinanceOperationTypeDTO type) : base(type)
    {

    }

    public override void ChangeFinanceOperationType(FinanceOperationTypeDTO type)
    {
        base.ChangeFinanceOperationType(type);

        if (type.EntryType != EntryType.Income)
            throw new ArgumentException(nameof(type));
    }
}
