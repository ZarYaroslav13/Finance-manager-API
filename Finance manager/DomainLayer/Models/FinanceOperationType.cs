using DataLayer.Models;

namespace DomainLayer.Models;

public class FinanceOperationType : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }
}
