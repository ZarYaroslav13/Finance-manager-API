namespace DataLayer.Models;

public class FinanceOperationType : Base.Entity
{
    public string Name { get; set; } = "";

    public string? Description { get; set; }

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public Wallet Wallet { get; set; }

    public List<FinanceOperation> FinanceOperations { get; set; } = default!;
}
