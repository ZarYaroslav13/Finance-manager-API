namespace DomainLayer.Models;

public class ExpenseModel : FinanceOperationModel
{
    public ExpenseModel(FinanceOperationTypeModel type) : base(type)
    {
        if (type.EntryType != DataLayer.Models.EntryType.Exponse)
            throw new ArgumentException(nameof(type));
    }
}
