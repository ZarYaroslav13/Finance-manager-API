using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Services.Finances;

public interface IFinanceReportCreator
{
    public Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime startDate, DateTime endDate);

    public Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime day);
}
