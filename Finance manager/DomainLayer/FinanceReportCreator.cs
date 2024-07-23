using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer;

public class FinanceReportCreator
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<DataLayer.Models.FinanceOperation> _repository;

    public FinanceReportCreator(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _repository = _unitOfWork
             .GetRepository<DataLayer.Models.FinanceOperation>();
    }

    public FinanceReport CreateReport(Wallet wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(nameof(wallet));
        ArgumentNullException.ThrowIfNull(nameof(startDate));
        ArgumentNullException.ThrowIfNull(nameof(endDate));

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReport() { Wallet = wallet, Period = period };

        report.Operations = _repository
             .GetAll(filter: t => startDate <= t.Date && t.Date <= endDate)
             .Select(_mapper.Map<FinanceOperation>)
             .ToList();

        return report;
    }

    public FinanceReport CreateReport(Wallet wallet, DateTime day)
    {
        return CreateReport(wallet, day, day);
    }
}
