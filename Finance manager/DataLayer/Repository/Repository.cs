using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repository;

public class Repository<T> : IRepository<T> where T : Models.Base.Entity
{
    internal DbContext _context;
    internal DbSet<T> _dbSet = default!;

    public Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException();
        _dbSet = _context.Set<T>() ?? throw new ArgumentNullException();
    }

    public virtual IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).AsNoTracking().ToList();
        }

        return query.AsNoTracking().ToList();
    }

    public virtual void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }
}