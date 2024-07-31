using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;
using System.Linq.Expressions;

namespace DomainLayer.Services;

public class CRUDService<T_Domain, T_DB> : ICRUDService<T_Domain, T_DB>
    where T_Domain : Models.Base.Model
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

    public List<T_Domain> GetAll(
        Func<IQueryable<T_DB>,
            IOrderedQueryable<T_DB>> orderBy = null,
        Expression<Func<T_DB, bool>> filter = null,
        params string[] includeProperties)
    {
        return _repository
            .GetAll(filter: filter,
                    orderBy: orderBy,
                    includeProperties: includeProperties)
            .Select(_mapper.Map<T_Domain>)
            .ToList();
    }

    public virtual T_Domain Find(int id)
    {
        return _mapper.Map<T_Domain>(_repository.GetById(id));
    }

    public virtual T_Domain Add(T_Domain entity)
    {
        _repository.Insert(_mapper.Map<T_DB>(entity));
        _unitOfWork.SaveChanges();

        return entity;
    }

    public virtual T_Domain Update(T_Domain entity)
    {
        var dbEntity = _repository.GetById(entity.Id);
        var mappedEntity = _mapper.Map<T_DB>(entity);

        foreach (var property in dbEntity.GetType().GetProperties())
        {
            property.SetValue(dbEntity, property.GetValue(mappedEntity));
        }

        _repository.Update(dbEntity);
        _unitOfWork.SaveChanges();

        return entity;
    }

    public virtual void Delete(T_Domain entity)
    {
        Update(entity);

        var dbEntity = _repository.GetById(entity.Id);
        
        _repository.Delete(dbEntity);
        _unitOfWork.SaveChanges();
    }
}
