using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;

namespace DomainLayer.Services.FinanceOperations;

public class FinanceOperationTypeService : EntityService<FinanceOperationTypeModel, FinanceOperationType>, IFinanceOperationTypeService
{
    private readonly IRepository<FinanceOperation> _financeOperationRepository;

    public FinanceOperationTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _financeOperationRepository = _unitOfWork.GetRepository<FinanceOperation>();
    }

    public List<FinanceOperationTypeModel> GetAllFinanceOperationTypesWithWalletId(int walletId)
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

    public List<FinanceOperationModel> GetAllFinanceOperationWithTypeId(int TypeId)
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
}
