using DataLayer.Repository;

namespace DataLayer.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : Models.Base.Entity;

    Task SaveChangesAsync();
}
