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
            return Operations;
        }
        set
        {
            Operations = value;

            CalculateTotalIncome();
            CalculateTotalExpense();
        }
    }

    public FinanceReport(int walletId, string walletName, Period period)
    {
        WalletName = walletName ?? throw new ArgumentNullException(nameof(walletName));
        WalletId = walletId;
        Period = period;
    }

    public Period Period { get; set; }

    private int CalculateTotalIncome()
    {
        TotalIncome = Operations
            .OfType<Income>()
            .Select(i => i.Amount)
            .Sum();

        return TotalIncome;
    }

    private int CalculateTotalExpense()
    {
        TotalExpense = Operations
            .OfType<Expense>()
            .Select(e => e.Amount)
            .Sum();

        return TotalExpense;
    }
}
