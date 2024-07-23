namespace DomainLayer.Models;

public abstract class FinanceOperation : Base.Model
{
    public int Amount { get; set; }

    public DateTime Date { get; set; }

    public FinanceOperationType Type { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(FinanceOperation))
            return false;

        var financeOperation = (FinanceOperation)obj;

        return Id == financeOperation.Id
            && Amount == financeOperation.Amount
            && Date == financeOperation.Date
            && Type == financeOperation.Type;
    }
}
