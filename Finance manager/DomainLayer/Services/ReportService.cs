using AutoMapper;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services;

public class ReportService
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public Report CreateReport(Wallet wallet, DateTime startDate, DateTime endDate)
    {
        var report = new Report() { StartDate = startDate, EndDate = endDate };

        report.AllOperations = _unitOfWork
                                        .GetRepository<DataLayer.Models.Transaction>()
                                        .GetAll()
                                        .Where(t => startDate <= t.Date && t.Date <= endDate)
                                        .Select(_mapper.Map<FinanceOperation>)
                                        .ToList();

        CalculateIncomeAndExpense(report, startDate, endDate);

        return report;
    }

    private void CalculateIncomeAndExpense(Report report, DateTime startDate, DateTime endDate)
    {
        foreach (var operation in report.AllOperations)
        {
            if (operation.GetType() == typeof(Income))
            {
                report.TotalIncome += operation.Amount;
                continue;
            }

            report.TotalExpense += operation.Amount;
        }
    }
}
