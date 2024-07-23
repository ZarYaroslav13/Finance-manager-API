using DataLayer.Models;

namespace DomainLayer.Models;

public class FinanceOperationType : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public string WalletName { get; set; } = String.Empty;

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
            && WalletName == financeOperationType.WalletName;
    }
}
