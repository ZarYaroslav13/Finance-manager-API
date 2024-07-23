namespace DomainLayer.Models;

public class Wallet : Base.Model
{
    public string Name { get; set; } = String.Empty;

    public int Balance { get; set; }

    public List<FinanceOperationType> FinanceOperationTypes { get; set; } = new();

    public List<Income> Incomes { get; set; } = default!;

    public List<Expense> Expenses { get; set; } = default!;

    public int AccountId { get; set; }
}
