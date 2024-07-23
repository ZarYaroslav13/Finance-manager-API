using DataLayer.Models.Base;

namespace DataLayer.Models;

public class FinanceOperation : Entity
{
    public uint Amount { get; set; }

    public DateTime Date { get; set; }

    public int TypeId { get; set; }

    public FinanceOperationType Type { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(FinanceOperation))
            return false;

        var financeOperation = (FinanceOperation)obj;

        return Id == financeOperation.Id
                && Amount == financeOperation.Amount
                && Date == financeOperation.Date
                && TypeId == financeOperation.TypeId
                && Type == financeOperation.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Amount, Date, TypeId, Type);
    }
}
