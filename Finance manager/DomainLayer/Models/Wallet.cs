namespace DomainLayer.Models;

public class Wallet : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public int Balance { get; set; }

    public List<FinanceOperationType> FinanceOperationTypes { get; set; } = new();

    public List<Income> Incomes { get; set; } = default!;

    public List<Expense> Expenses { get; set; } = default!;

    public int AccountId { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Wallet))
            return false;

        Wallet wallet = (Wallet)obj;

        return Id == wallet.Id
            && Balance == wallet.Balance
            && AccountId == wallet.AccountId
            && Enumerable.SequenceEqual(FinanceOperationTypes, wallet.FinanceOperationTypes)
            && Enumerable.SequenceEqual(Incomes, wallet.Incomes)
            && Enumerable.SequenceEqual(Expenses, wallet.Expenses);
    }
}
