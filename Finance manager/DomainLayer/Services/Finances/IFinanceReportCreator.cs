using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public interface IFinanceReportCreator
{
    public Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime startDate, DateTime endDate);

    public Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime day);
}
