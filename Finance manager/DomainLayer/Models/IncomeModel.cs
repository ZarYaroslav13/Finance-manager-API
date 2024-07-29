namespace DomainLayer.Models;

public class IncomeModel : FinanceOperationModel
{
    public IncomeModel(FinanceOperationTypeModel type) : base(type)
    {
        if (type.EntryType != DataLayer.Models.EntryType.Income)
            throw new ArgumentException(nameof(type));
    }
}
