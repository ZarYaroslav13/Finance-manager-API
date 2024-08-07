using DomainLayer.Models;

namespace Finance_manager_API.Models;

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
        List<FinanceOperationDTO> operations,
        Period period)
    {
        WalletId = walletId;
        WalletName = walletName ?? throw new ArgumentNullException(nameof(walletName));
        TotalIncome = totalIncome;
        TotalExpense = totalExpense;
        Operations = operations ?? throw new ArgumentNullException(nameof(operations));
        Period = period;
    }
}