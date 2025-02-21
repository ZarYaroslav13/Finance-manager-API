using FinanceManager.Domain.Models;

namespace FinanceManager.Domain.Services.Finances;

public class FinanceReportCreator : IFinanceReportCreator
{
    protected readonly IFinanceService _service;

    public FinanceReportCreator(IFinanceService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public async Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(wallet);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReportModel(wallet.Id, wallet.Name, period);
        var allOperations = await _service.GetAllFinanceOperationOfWalletAsync(wallet.Id, startDate, endDate);

        report.Operations = allOperations;

        return report;
    }

    public async Task<FinanceReportModel> CreateFinanceReportAsync(WalletModel wallet, DateTime day)
    {
        return await CreateFinanceReportAsync(wallet, day, day);
    }
}
