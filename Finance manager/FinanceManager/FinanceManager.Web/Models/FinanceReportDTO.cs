using DomainLayer.Models;

namespace FinanceManager.Web.Models;

public class FinanceReportDTO : Base.ModelDTO
{
    public int WalletId { get; }
    public string WalletName { get; } = String.Empty;
    public int TotalIncome { get; }
    public int TotalExpense { get; }
    public List<FinanceOperationDTO> Operations { get; }
    public Period Period { get; }

    public FinanceReportDTO(
        int walletId,
        string walletName,
        int totalIncome,
        int totalExpense,
        List<FinanceOperationDTO> operation,
        Period period)
    {
        WalletId = walletId;
        WalletName = walletName ?? throw new ArgumentNullException(nameof(walletName));
        TotalIncome = totalIncome;
        TotalExpense = totalExpense;
        Operations = operation ?? throw new ArgumentNullException(nameof(operation));
        Period = period;
    }

    public override bool Equals(object? obj)
    {
        if (!base.Equals(obj))
            return false;

        var financeReport = (FinanceReportDTO)obj;

        return WalletId == financeReport.WalletId
            && WalletName == financeReport.WalletName
            && TotalIncome == financeReport.TotalIncome
            && TotalExpense == financeReport.TotalExpense
            && Period == financeReport.Period
            && AreEqualLists(Operations, financeReport.Operations);
    }

    public override int GetHashCode()
    {
        int operationHashCode = GetHashCodeOfList(Operations);

        return HashCode.Combine(base.GetHashCode(), WalletId, WalletName, TotalIncome, TotalExpense, Period, operationHashCode);
    }
}
