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

    public async Task<List<FinanceOperationTypeModel>> GetAllFinanceOperationTypesOfWalletAsync(int walletId)
    {
        return (await _financeOperationTypeRepository
                .GetAllAsync(filter: fot => fot.WalletId == walletId))
                .Select(_mapper.Map<FinanceOperationTypeModel>)
                .ToList();
    }

    public async Task<FinanceOperationTypeModel> AddFinanceOperationTypeAsync(FinanceOperationTypeModel type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.Id != 0)
            throw new ArgumentException(nameof(type));

        var result = _mapper.Map<FinanceOperationTypeModel>(
                         _financeOperationTypeRepository.Insert(
                             _mapper.Map<FinanceOperationType>(type)));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task<FinanceOperationTypeModel> UpdateFinanceOperationTypeAsync(FinanceOperationTypeModel type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.Id == 0)
            throw new ArgumentException(nameof(type));

        var result = _mapper.Map<FinanceOperationTypeModel>(
                         (await _financeOperationTypeRepository.UpdateAsync(
                            _mapper.Map<FinanceOperationType>(type))));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteFinanceOperationTypeAsync(int id)
    {
        if ((await _financeOperationRepository.GetAllAsync(
                includeProperties: nameof(FinanceOperation.Type),
                filter: fo => fo.Type.Id == id))
            .Any())
        {
            throw new InvalidOperationException();
        }

        _financeOperationTypeRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }

    #endregion

    #region FinanceOperationMethods

    public async Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, int index = 0, int count = 0)
    {
        if (walletId <= 0)
            throw new ArgumentOutOfRangeException(nameof(walletId));

        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        List<FinanceOperationModel> result = (await _financeOperationRepository
                .GetAllAsync(
                   includeProperties: nameof(FinanceOperation.Type),
                    filter: fo => fo.Type.WalletId == walletId,
                    orderBy: iQ => iQ.OrderBy(fo => fo.Date),
                    skip: count,
                    take: index))
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();

        return result;
    }

    public async Task<List<FinanceOperationModel>> GetAllFinanceOperationOfWalletAsync(int walletId, DateTime startDate, DateTime endDate)
    {
        if (walletId <= 0)
            throw new ArgumentException(nameof(walletId));

        ArgumentOutOfRangeException.ThrowIfGreaterThan(startDate, endDate);

        List<FinanceOperationModel> result = (await _financeOperationRepository
                .GetAllAsync(
                includeProperties: nameof(FinanceOperation.Type),
                filter: fo =>
                       fo.Type.WalletId == walletId
                    && fo.Date <= endDate
                    && fo.Date >= startDate))
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();

        return result;
    }

    public async Task<List<FinanceOperationModel>> GetAllFinanceOperationOfTypeAsync(int TypeId)
    {
        return (await _financeOperationRepository
                .GetAllAsync(filter: fo => fo.TypeId == TypeId))
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();
    }

    public async Task<FinanceOperationModel> AddFinanceOperationAsync(FinanceOperationModel financeOperation)
    {
        ArgumentNullException.ThrowIfNull(financeOperation);

        if (financeOperation.Id != 0)
            throw new ArgumentException(nameof(financeOperation));

        var financeOperationForDb = _mapper.Map<FinanceOperation>(financeOperation);

        if ((await _financeOperationRepository.GetAllAsync(filter: fo => fo == financeOperationForDb))
                .Any())
            throw new InvalidOperationException();

        var result = _mapper.Map<FinanceOperationModel>(
                        _financeOperationRepository.Insert(financeOperationForDb));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task<FinanceOperationModel> UpdateFinanceOperationAsync(FinanceOperationModel financeOperation)
    {
        ArgumentNullException.ThrowIfNull(financeOperation);

        if (financeOperation.Id == 0)
            throw new ArgumentException(nameof(financeOperation));

        var result = _mapper.Map<FinanceOperationModel>(
                        await _financeOperationRepository.UpdateAsync(
                            _mapper.Map<FinanceOperation>(financeOperation)));
        await _unitOfWork.SaveChangesAsync();

        return result;
    }

    public async Task DeleteFinanceOperationAsync(int id)
    {
        _financeOperationRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }
    #endregion
}
