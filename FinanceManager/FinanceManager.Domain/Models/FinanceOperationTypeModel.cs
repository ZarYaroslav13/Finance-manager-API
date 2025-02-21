using Infrastructure.Models;

namespace FinanceManager.Domain.Models;

public class FinanceOperationTypeModel : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public string WalletName { get; set; } = String.Empty;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeOperationType = (FinanceOperationTypeModel)obj;

        return Name == financeOperationType.Name
            && Description == financeOperationType.Description
            && EntryType == financeOperationType.EntryType
            && WalletId == financeOperationType.WalletId
            && WalletName == financeOperationType.WalletName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Name, Description, EntryType, WalletId, WalletName);
    }
}
