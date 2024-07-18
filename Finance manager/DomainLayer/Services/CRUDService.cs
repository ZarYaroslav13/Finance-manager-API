using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;

namespace DomainLayer.Services;

public class CRUDService<T_Domain, T_DB> : ICRUDService<T_Domain, T_DB>
    where T_Domain : Models.Base.Entity
    where T_DB : DataLayer.Models.Base.Entity
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<T_DB> _repository;

    public CRUDService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _repository = _unitOfWork.GetRepository<T_DB>();
    }

    public virtual List<T_Domain> GetAll()
    {
        return _repository.GetAll().Select(_mapper.Map<T_Domain>).ToList();
    }

    public virtual T_Domain Find(int id)
    {
        return _mapper.Map<T_Domain>(_repository.GetById(id));
    }

    public virtual T_Domain Add(T_Domain entity)
    {
        _repository.Insert(_mapper.Map<T_DB>(entity));

        return entity;
    }

    public virtual T_Domain Update(T_Domain entity)
    {
        _repository.Update(_mapper.Map<T_DB>(entity));

        return entity;
    }

    public virtual void Delete(T_Domain entity)
    {
        _repository.Delete(_mapper.Map<T_DB>(entity));
    }
}
