namespace DomainLayer.Models;

public class Income : FinanceOperation
{
    public Income(FinanceOperationType type) : base(type)
    {
        if (type.EntryType != DataLayer.Models.EntryType.Income)
            throw new ArgumentException(nameof(type));
    }
}
