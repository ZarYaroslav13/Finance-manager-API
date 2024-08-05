using AutoMapper;
using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return _mapper.Map<FinanceOperationTypeModel>(
                _repository.Insert(
                    _mapper.Map<FinanceOperationType>(type)));
    }

    public FinanceOperationTypeModel UpdateFinanceOperationType(FinanceOperationTypeModel type)
    {
        return _mapper.Map<FinanceOperationTypeModel>(
                _repository.Update(
                    _mapper.Map<FinanceOperationType>(type)));
    }

    public void DeleteFinanceOperationType(int id)
    {
        _repository.Delete(id);
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
        return _mapper.Map<FinanceOperationModel>(
                _financeOperationRepository.Insert(
                    _mapper.Map<FinanceOperation>(financeOperation)));
    }

    public FinanceOperationModel UpdateFinanceOperationType(FinanceOperationModel financeOperation)
    {
        return _mapper.Map<FinanceOperationModel>(
                _financeOperationRepository.Update(
                    _mapper.Map<FinanceOperation>(financeOperation)));
    }

    public void DeleteFinanceOperation(int id)
    {
        _financeOperationRepository.Delete(id);
    }
}
