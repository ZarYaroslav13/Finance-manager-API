namespace DataLayer.Models;

public class TransactionType : Base.Entity
{
    public string Caption { get; set; }

    public string? Description { get; set; }

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public Wallet Wallet { get; set; }
}
