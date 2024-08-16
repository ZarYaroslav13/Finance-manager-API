﻿using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public class FinanceService : BaseService, IFinanceService
{
    private readonly IRepository<FinanceOperation> _financeOperationRepository;
    private readonly IRepository<FinanceOperationType> _financeOperationTypeRepository;

    public FinanceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _financeOperationRepository = _unitOfWork.GetRepository<FinanceOperation>();
        _financeOperationTypeRepository = _unitOfWork.GetRepository<FinanceOperationType>();
    }

    #region FinanceOperationTypeMethods

    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesOfWallet(int walletId)
    {
        return _financeOperationTypeRepository
                .GetAll(filter: fot => fot.WalletId == walletId)
                .Select(_mapper.Map<FinanceOperationTypeModel>)
                .ToList();
    }

    public FinanceOperationTypeModel AddFinanceOperationType(FinanceOperationTypeModel type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.Id != 0)
            throw new ArgumentException(nameof(type));

        var result = _mapper.Map<FinanceOperationTypeModel>(
                         _financeOperationTypeRepository.Insert(
                             _mapper.Map<FinanceOperationType>(type)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.Id == 0)
            throw new ArgumentException(nameof(type));

        var result = _mapper.Map<FinanceOperationTypeModel>(
                         _financeOperationTypeRepository.Update(
                            _mapper.Map<FinanceOperationType>(type)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void DeleteFinanceOperationType(int id)
    {
        if (_financeOperationRepository.GetAll(
                includeProperties: nameof(FinanceOperation.Type),
                filter: fo => fo.Type.Id == id)
            .Any())
        {
            throw new InvalidOperationException();
        }

        _financeOperationTypeRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    #endregion

    #region FinanceOperationMethods

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId, int count = 0, int index = 0)
    {
        if (walletId <= 0)
            throw new ArgumentException(nameof(walletId));

        if (count < 0)
            throw new ArgumentException(nameof(count));

        List<FinanceOperationModel> result = new();

        if (count == 0)
        {
            foreach (var type in GetAllFinanceOperationTypesOfWallet(walletId))
            {
                result.AddRange(GetAllFinanceOperationOfType(type.Id));
            }
        }

        if (count > 0)
        {
            result = _financeOperationRepository
                .GetAll(
                   includeProperties: nameof(FinanceOperation.Type),
                    filter: fo => fo.Type.WalletId == walletId, orderBy:
                    iQ => iQ.OrderBy(fo => fo.Date),
                    skip: count,
                    take: index)
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();

            return result;
        }

        return result;
    }

    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId, DateTime startDate, DateTime endDate)
    {
        if (walletId <= 0)
            throw new ArgumentException(nameof(walletId));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        List<FinanceOperationModel> result = new();

        foreach (var type in GetAllFinanceOperationTypesOfWallet(walletId))
        {
            result.AddRange(_financeOperationRepository
                .GetAll(filter: fo =>
                       fo.TypeId == type.Id
                    && fo.Date <= endDate
                    && fo.Date >= startDate)
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList());
        }

        return result;
    }

    public List<FinanceOperationModel> GetAllFinanceOperationOfType(int TypeId)
    {
        return _financeOperationRepository
                .GetAll(filter: fo => fo.TypeId == TypeId)
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();
    }

    public FinanceOperationModel AddFinanceOperation(FinanceOperationModel financeOperation)
    {
        ArgumentNullException.ThrowIfNull(financeOperation);

        if (financeOperation.Id != 0)
            throw new ArgumentException(nameof(financeOperation));

        var financeOperationForDb = _mapper.Map<FinanceOperation>(financeOperation);

        if (_financeOperationRepository.GetAll(filter: fo => fo == financeOperationForDb).Any())
            throw new InvalidOperationException();

        var result = _mapper.Map<FinanceOperationModel>(
                        _financeOperationRepository.Insert(financeOperationForDb));
        _unitOfWork.SaveChanges();

        return result;
    }

    public FinanceOperationModel UpdateFinanceOperation(FinanceOperationModel financeOperation)
    {
        ArgumentNullException.ThrowIfNull(financeOperation);

        if (financeOperation.Id == 0)
            throw new ArgumentException(nameof(financeOperation));

        var result = _mapper.Map<FinanceOperationModel>(
                        _financeOperationRepository.Update(
                            _mapper.Map<FinanceOperation>(financeOperation)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void DeleteFinanceOperation(int id)
    {
        _financeOperationRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }
    #endregion
}
