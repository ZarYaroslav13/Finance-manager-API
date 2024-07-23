using DomainLayer.Models.Base;

namespace DomainLayer.Models;

public class FinanceReport : Model
{
    public int WalletId { get; }

    public string WalletName { get; } = String.Empty;

    public int TotalIncome { get; private set; }

    public int TotalExpense { get; private set; }

    public List<FinanceOperation> Operations
    {
        get
        {
            return _operations;
        }
        set
        {
            _operations = value;

            CalculateTotalIncome();
            CalculateTotalExpense();
        }
    }

    private List<FinanceOperation> _operations { get; set; } = new();

    public FinanceReport(int walletId, string walletName, Period period)
    {
        WalletName = walletName ?? throw new ArgumentNullException(nameof(walletName));
        WalletId = walletId;
        Period = period;
    }

    public Period Period { get; set; }

    private int CalculateTotalIncome()
    {
        TotalIncome = _operations
            .OfType<Income>()
            .Select(i => i.Amount)
            .Sum();

        return TotalIncome;
    }

    private int CalculateTotalExpense()
    {
        TotalExpense = _operations
            .OfType<Expense>()
            .Select(e => e.Amount)
            .Sum();

        return TotalExpense;
    }
}
