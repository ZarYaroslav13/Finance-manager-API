using AutoMapper;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services.FinanceOperations;

namespace DomainLayer;

public class FinanceReportCreator
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IFinanceOperationTypeService _service;

    public FinanceReportCreator(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _service = new FinanceOperationTypeService(_unitOfWork, _mapper);
    }

    public FinanceReportModel CreateFinanceReport(WalletModel wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(wallet);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReportModel(wallet.Id, wallet.Name, period);
        var allOperations = new List<FinanceOperationModel>();
        foreach (var fot in _service.GetAllFinanceOperationTypesWithWalletId(wallet.Id))
        {
            var operations = _service.GetAllFinanceOperationWithTypeId(fot.Id);
            operations.ForEach(fo => fo.ChangeFinanceOperationType(fot));

            allOperations.AddRange(operations);
        }

        report.Operations = allOperations;

        return report;
    }

    public FinanceReportModel CreateFinanceReport(WalletModel wallet, DateTime day)
    {
        return CreateFinanceReport(wallet, day, day);
    }
}
