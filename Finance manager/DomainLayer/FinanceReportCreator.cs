using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services;
using System.Linq.Expressions;

namespace DomainLayer;

public class FinanceReportCreator
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ICRUDService<FinanceOperation, DataLayer.Models.FinanceOperation> _service;

    public FinanceReportCreator(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _service = new CRUDService<FinanceOperation, DataLayer.Models.FinanceOperation>(_unitOfWork, _mapper);
    }

    public FinanceReport CreateReport(Wallet wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(nameof(wallet));
        ArgumentNullException.ThrowIfNull(nameof(startDate));
        ArgumentNullException.ThrowIfNull(nameof(endDate));

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReport(wallet.Id, wallet.Name, period);

        report.Operations = _service
             .GetAll(filter: t => startDate <= t.Date && t.Date <= endDate)
             .ToList();

        return report;
    }

    public FinanceReport CreateReport(Wallet wallet, DateTime day)
    {
        return CreateReport(wallet, day, day);
    }
}
