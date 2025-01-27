namespace Infrastructure.Models;

public class FinanceOperationType : Base.Entity
{
    public string Name { get; set; } = "";

    public string? Description { get; set; }

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public Wallet Wallet { get; set; }

    public List<FinanceOperation> FinanceOperations { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeOperationType = (FinanceOperationType)obj;

        return Name == financeOperationType.Name
                && Description == financeOperationType.Description
                && EntryType == financeOperationType.EntryType
                && WalletId == financeOperationType.WalletId
                && AreEqualLists(FinanceOperations, financeOperationType.FinanceOperations);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Name, Description, EntryType, WalletId, Wallet, GetHashCodeOfList(FinanceOperations));
    }
}
