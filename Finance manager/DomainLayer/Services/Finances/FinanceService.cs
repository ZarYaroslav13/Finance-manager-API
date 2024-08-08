using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services.Finances;

public class FinanceService : EntityService<FinanceOperationTypeModel, FinanceOperationType>, IFinanceService
{
    private readonly IRepository<FinanceOperation> _financeOperationRepository;

    public FinanceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _financeOperationRepository = _unitOfWork.GetRepository<FinanceOperation>();
    }

    #region FinanceOperationTypeMethods

    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesOfWallet(int walletId)
    {
        return _repository
                .GetAll(filter: fot => fot.WalletId == walletId)
                .Select(_mapper.Map<FinanceOperationTypeModel>)
                .ToList();
    }

    public FinanceOperationTypeModel AddNewFinanceOperationType(FinanceOperationTypeModel type)
    {
        var result = _mapper.Map<FinanceOperationTypeModel>(
                         _repository.Insert(
                            _mapper.Map<FinanceOperationType>(type)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type)
    {
        var result = _mapper.Map<FinanceOperationTypeModel>(
                         _repository.Update(
                            _mapper.Map<FinanceOperationType>(type)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public void DeleteFinanceOperationType(int id)
    {
        _repository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    #endregion

    #region FinanceOperationMethods
    public List<FinanceOperationModel> GetAllFinanceOperationOfWallet(int walletId)
    {
        List<FinanceOperationModel> result = new();

        foreach (var type in GetAllFinanceOperationTypesOfWallet(walletId))
        {
            result.AddRange(GetAllFinanceOperationOfType(type.Id));
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

    public FinanceOperationModel AddNewFinanceOperationType(FinanceOperationModel financeOperation)
    {
        var result = _mapper.Map<FinanceOperationModel>(
                        _financeOperationRepository.Insert(
                            _mapper.Map<FinanceOperation>(financeOperation)));
        _unitOfWork.SaveChanges();

        return result;
    }

    public FinanceOperationModel UpdateFinanceOperationType(FinanceOperationModel financeOperation)
    {
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
