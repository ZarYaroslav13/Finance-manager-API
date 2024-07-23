namespace DataLayer.Models;

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
        if (obj == null || obj.GetType() != typeof(FinanceOperationType))
            return false;

        var financeOperationType = (FinanceOperationType)obj;

        return Id == financeOperationType.Id
                && Name == financeOperationType.Name
                && Description == financeOperationType.Description
                && EntryType == financeOperationType.EntryType
                && WalletId == financeOperationType.WalletId
                && AreEqualLists(FinanceOperations, financeOperationType.FinanceOperations);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, EntryType, WalletId, Wallet, FinanceOperations);
    }
}
