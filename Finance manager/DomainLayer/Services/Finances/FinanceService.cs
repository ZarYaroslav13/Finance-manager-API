using AutoMapper;
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

    public async Task<bool> IsAccountOwnerOfWalletAsync(int accountid, int walletId)
    {
        if (accountid <= 0 || walletId <= 0)
            throw new ArgumentOutOfRangeException($"{nameof(accountid)} and {nameof(walletId)} must be above zero");

        var wallet = await _unitOfWork.GetRepository<Wallet>().GetByIdAsync(walletId);

        return wallet.AccountId == accountid;
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
                         (_financeOperationTypeRepository.Update(
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
            throw new InvalidOperationException($"Deleting type with Id: {id} is imposible through operations with this type exists");
        }

        _financeOperationTypeRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsAccountOwnerOfFinanceOperationTypeAsync(int accountid, int typeId)
    {
        if (accountid <= 0 || typeId <= 0)
            throw new ArgumentOutOfRangeException($"{nameof(accountid)} and {nameof(typeId)} must be above zero");

        var type = await _financeOperationTypeRepository.GetByIdAsync(typeId);
        var wallet = await _unitOfWork.GetRepository<Wallet>().GetByIdAsync(type.WalletId);

        return wallet.AccountId == accountid;
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

        var dayAfterEndDate = endDate.AddDays(1);
        var dayBeforeStartDate = startDate.AddDays(-1);

        var result = (await _financeOperationRepository
                .GetAllAsync(
                includeProperties: nameof(FinanceOperation.Type),
                filter: fo =>
                       fo.Type.WalletId == walletId
                    && fo.Date <= dayAfterEndDate
                    && fo.Date >= dayBeforeStartDate))
                .Select(_mapper.Map<FinanceOperationModel>)
                .ToList();

        return result;
    }

    public async Task<List<FinanceOperationModel>> GetAllFinanceOperationOfTypeAsync(int TypeId)
    {

        string[] includedProperities =
        {
            nameof(FinanceOperation.Type)
        };

        return (await _financeOperationRepository
                .GetAllAsync(filter: fo => fo.TypeId == TypeId, includeProperties: includedProperities))
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

        var dbResult = _financeOperationRepository.Insert(financeOperationForDb);
        await _unitOfWork.SaveChangesAsync();

        dbResult.Type = await _financeOperationTypeRepository.GetByIdAsync(dbResult.TypeId);
        var result = _mapper.Map<FinanceOperationModel>(dbResult);

        return result;
    }

    public async Task<FinanceOperationModel> UpdateFinanceOperationAsync(FinanceOperationModel financeOperation)
    {
        ArgumentNullException.ThrowIfNull(financeOperation);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(financeOperation.Id);
        var dbResult = _financeOperationRepository.Update(
                            _mapper.Map<FinanceOperation>(financeOperation));
        await _unitOfWork.SaveChangesAsync();

        dbResult.Type = await _financeOperationTypeRepository.GetByIdAsync(dbResult.TypeId);
        var result = _mapper.Map<FinanceOperationModel>(dbResult);

        return result;
    }

    public async Task DeleteFinanceOperationAsync(int id)
    {
        _financeOperationRepository.Delete(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsAccountOwnerOfFinanceOperationAsync(int accountid, int operationId)
    {
        if (accountid <= 0 || operationId <= 0)
            throw new ArgumentOutOfRangeException($"{nameof(accountid)} and {nameof(operationId)} must be above zero");

        var operation = await _financeOperationRepository.GetByIdAsync(operationId);
        var type = await _financeOperationTypeRepository.GetByIdAsync(operation.TypeId);
        var wallet = await _unitOfWork.GetRepository<Wallet>().GetByIdAsync(type.WalletId);

        return wallet.AccountId == accountid;
    }
    #endregion
}
