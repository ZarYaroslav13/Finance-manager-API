using DataLayer.Models.Base;
using DataLayer.Repository;

namespace DataLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool disposed;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public Repository<T> GetRepository<T>() where T : Entity
    {
        return new Repository<T>(_context);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
        {
            _context.Dispose();
        }
        this.disposed = true;
    }


}
