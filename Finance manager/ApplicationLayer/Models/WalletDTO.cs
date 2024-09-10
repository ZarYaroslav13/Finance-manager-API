using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Models;

public class WalletDTO : Base.ModelDTO
{
    [Required]
    public string Name
    {
        get { return _name; }
        set { _name = value.Trim(); }
    }

    public int Balance { get; set; } = 0;

    public List<FinanceOperationTypeDTO> FinanceOperationTypes { get; set; } = default!;

    public List<IncomeDTO> Incomes { get; set; } = default!;

    public List<ExpenseDTO> Expenses { get; set; } = default!;

    [Required]
    [Range(1, int.MaxValue)]
    public int AccountId { get; set; }

    private string _name = string.Empty;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        WalletDTO wallet = (WalletDTO)obj;

        return Id == wallet.Id
                && Name == wallet.Name
                && Balance == wallet.Balance
                && AccountId == wallet.AccountId
                && AreEqualLists(FinanceOperationTypes, wallet.FinanceOperationTypes)
                && AreEqualLists(Incomes, wallet.Incomes)
                && AreEqualLists(Expenses, wallet.Expenses);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Balance, AccountId, FinanceOperationTypes, Incomes, Expenses);
    }
}
