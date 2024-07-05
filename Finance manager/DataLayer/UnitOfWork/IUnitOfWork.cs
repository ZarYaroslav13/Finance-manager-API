using DataLayer.Repository;

namespace DataLayer.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Repository<T> GetRepository<T>() where T : Models.Base.Entity;

    void SaveChanges();

    void Dispose();
}
