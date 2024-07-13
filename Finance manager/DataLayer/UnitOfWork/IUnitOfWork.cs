using DataLayer.Repository;

namespace DataLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : Models.Base.Entity;

    void SaveChanges();
}
