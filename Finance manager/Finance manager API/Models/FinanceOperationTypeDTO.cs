using DataLayer.Models;

namespace Finance_manager.Models;

public class FinanceOperationTypeDTO : Base.ModelDTO
{
    public string Name { get; set; } = String.Empty;

    public string Description { get; set; } = String.Empty;

    public EntryType EntryType { get; set; }

    public int WalletId { get; set; }

    public string WalletName { get; set; } = String.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var financeOperationType = (FinanceOperationTypeDTO)obj;

        return Id == financeOperationType.Id
            && Name == financeOperationType.Name
            && Description == financeOperationType.Description
            && EntryType == financeOperationType.EntryType
            && WalletId == financeOperationType.WalletId
            && WalletName == financeOperationType.WalletName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, EntryType, WalletId, WalletName);
    }
}
