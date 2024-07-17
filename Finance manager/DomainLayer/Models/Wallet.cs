namespace DomainLayer.Models;

public class Wallet : Base.Entity
{
    public int Balance { get; set; }

    public List<FinanceOperationType> OperationTypes { get; set; } = default!;

    public List<Income> Incomes { get; set; } = default!;

    public List<Expense> Expenses { get; set; } = default!;
}
