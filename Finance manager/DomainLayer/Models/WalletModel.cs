namespace DomainLayer.Models;

public class WalletModel : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public int Balance { get; set; }

    public List<FinanceOperationTypeModel> FinanceOperationTypes { get; set; } = default!;

    public List<IncomeModel> Incomes { get; set; } = default!;

    public List<ExpenseModel> Expenses { get; set; } = default!;

    public int AccountId { get; set; }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        WalletModel wallet = (WalletModel)obj;

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
