using DomainLayer.Models;
using DomainLayer.Services.FinanceOperations;

namespace DomainLayer;

public class FinanceReportCreator
{
    protected readonly IFinanceService _service;

    public FinanceReportCreator(IFinanceService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public FinanceReportModel CreateFinanceReport(WalletModel wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(wallet);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReportModel(wallet.Id, wallet.Name, period);
        var allOperations = _service.GetAllFinanceOperationOfWallet(wallet.Id);

        report.Operations = allOperations;

        return report;
    }

    public FinanceReportModel CreateFinanceReport(WalletModel wallet, DateTime day)
    {
        return CreateFinanceReport(wallet, day, day);
    }
}
