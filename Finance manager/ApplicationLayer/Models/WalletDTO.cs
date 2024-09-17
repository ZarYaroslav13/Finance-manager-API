using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Models;

public class WalletDTO : Base.ModelDTO
{
    [Required]
    [Length(2, 50)]
    public string Name
    {
        get { return _name; }
        set { _name = value.Trim(); }
    }

    public int Balance { get; set; } = 0;

    public List<FinanceOperationTypeDTO> FinanceOperationTypes { get; set; } = default!;

    public List<IncomeDTO> Incomes { get; set; } = default!;


    public List<ExpenseDTO> Expenses { get; set; } = default!;

    public int AccountId { get; set; }

    private string _name = string.Empty;

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        WalletDTO wallet = (WalletDTO)obj;

        return Name == wallet.Name
                && Balance == wallet.Balance
                && AccountId == wallet.AccountId
                && AreEqualLists(FinanceOperationTypes, wallet.FinanceOperationTypes)
                && AreEqualLists(Incomes, wallet.Incomes)
                && AreEqualLists(Expenses, wallet.Expenses);
    }

    public override int GetHashCode()
    {
        var financeOperationTypesHashValue = GetHashCodeOfList(FinanceOperationTypes);
        var incomesHashValue = GetHashCodeOfList(Incomes);
        var expensesHashValue = GetHashCodeOfList(Expenses);

        return HashCode.Combine(base.GetHashCode(), Name, Balance, AccountId, financeOperationTypesHashValue, incomesHashValue, expensesHashValue);
    }
}
