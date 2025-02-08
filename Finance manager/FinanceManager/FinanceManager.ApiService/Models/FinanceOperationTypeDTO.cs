using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.ApiService.Models;

public class FinanceOperationTypeDTO : Base.ModelDTO
{
    [Required]
    [Length(2, 50)]
    public string Name
    {
        get { return _name; }
        set { _name = value.Trim(); }
    }

    public string Description { get; set; } = String.Empty;

    [Required]
    public EntryType EntryType { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int WalletId { get; set; }

    public string WalletName
    {
        get { return _walletName; }
        set { _walletName = value.Trim(); }
    }

    private string _name = string.Empty;
    private string _walletName = string.Empty;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeOperationType = (FinanceOperationTypeDTO)obj;

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