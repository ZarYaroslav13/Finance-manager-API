﻿using AutoMapper;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using DomainLayer.Services;

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

    public FinanceReport CreateFinanceReport(Wallet wallet, DateTime startDate, DateTime endDate)
    {
        ArgumentNullException.ThrowIfNull(wallet);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        Period period = new() { StartDate = startDate, EndDate = endDate };

        var report = new FinanceReport(wallet.Id, wallet.Name, period);

        report.Operations = _service
             .GetAll(
                includeProperties: new string[] { nameof(DataLayer.Models.FinanceOperation.Type), nameof(DataLayer.Models.FinanceOperation.Type) + '.' + nameof(DataLayer.Models.FinanceOperation.Type.Wallet) },
                filter: t => startDate.Date <= t.Date.Date
                    && t.Date.Date <= endDate.Date
                    && t.Type.WalletId == report.WalletId)
             .ToList();

        return report;
    }

    public FinanceReport CreateFinanceReport(Wallet wallet, DateTime day)
    {
        return CreateFinanceReport(wallet, day, day);
    }
}
