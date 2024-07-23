namespace DomainLayer.Models;

public class Expense : FinanceOperation
{
    public Expense(FinanceOperationType type) : base(type)
    {
        if (type.EntryType != DataLayer.Models.EntryType.Exponse)
            throw new ArgumentException(nameof(type));
    }
}
