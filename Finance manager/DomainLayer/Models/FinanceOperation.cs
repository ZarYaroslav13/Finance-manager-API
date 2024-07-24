namespace DomainLayer.Models;

public abstract class FinanceOperation : Base.Model
{
    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public FinanceOperationType Type { get; private set; } = new();

    public FinanceOperation(FinanceOperationType type)
    {
        ChangeFinanceOperationType(type);
    }

    public void ChangeFinanceOperationType(FinanceOperationType type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType().BaseType != typeof(FinanceOperation))
            return false;

        var financeOperation = (FinanceOperation)obj;

        return Id == financeOperation.Id
            && Amount == financeOperation.Amount
            && Date == financeOperation.Date
            && Type == financeOperation.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Amount, Date, Type);
    }
}
