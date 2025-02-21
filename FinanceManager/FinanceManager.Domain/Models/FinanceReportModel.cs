using FinanceManager.Domain.Models.Base;

namespace FinanceManager.Domain.Models;

public class FinanceReportModel : Model
{
    public int WalletId { get; }
    public string WalletName { get; } = String.Empty;
    public int TotalIncome { get; private set; }
    public int TotalExpense { get; private set; }
    public List<FinanceOperationModel> Operations
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
    public Period Period { get; set; }

    private List<FinanceOperationModel> _operations { get; set; } = new();

    public FinanceReportModel(int walletId, string walletName, Period period)
    {
        WalletName = walletName ?? throw new ArgumentNullException(nameof(walletName));
        WalletId = walletId;
        Period = period;
    }

    private int CalculateTotalIncome()
    {
        TotalIncome = _operations
            .OfType<IncomeModel>()
            .Select(i => i.Amount)
            .Sum();

        return TotalIncome;
    }

    private int CalculateTotalExpense()
    {
        TotalExpense = _operations
            .OfType<ExpenseModel>()
            .Select(e => e.Amount)
            .Sum();

        return TotalExpense;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeReport = (FinanceReportModel)obj;

        return WalletId == financeReport.WalletId
            && WalletName == financeReport.WalletName
            && TotalIncome == financeReport.TotalIncome
            && TotalExpense == financeReport.TotalExpense
            && Period == financeReport.Period
            && AreEqualLists(Operations, financeReport.Operations);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), WalletId, WalletName, TotalIncome, TotalExpense, GetHashCodeOfList(Operations));
    }
}
