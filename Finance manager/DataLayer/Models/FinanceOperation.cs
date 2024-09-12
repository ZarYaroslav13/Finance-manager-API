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
        if (!base.Equals(obj))
            return false;

        var financeOperation = (FinanceOperation)obj;

        return Amount == financeOperation.Amount
                && Date == financeOperation.Date
                && TypeId == financeOperation.TypeId
                && Type == financeOperation.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Amount, Date, TypeId, Type);
    }
}
