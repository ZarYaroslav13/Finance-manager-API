using DataLayer.Models.Base;
using DataLayer.Repository;

namespace DataLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException();
    }

    public IRepository<T> GetRepository<T>() where T : Entity => new Repository<T>(_context);

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
