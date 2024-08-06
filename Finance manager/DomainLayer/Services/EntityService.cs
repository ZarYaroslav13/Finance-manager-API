using AutoMapper;
using DataLayer.Repository;
using DataLayer.UnitOfWork;

namespace DomainLayer.Services;

public abstract class EntityService<T_Domain, T_DB>
    where T_Domain : Models.Base.Model
    where T_DB : DataLayer.Models.Base.Entity
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<T_DB> _repository;

    public EntityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _repository = _unitOfWork.GetRepository<T_DB>();
    }
}
