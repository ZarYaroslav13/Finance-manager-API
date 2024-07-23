using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null,
                Expression<Func<T, bool>> filter = null,
                params string[] includeProperties)
    {
        IQueryable<T> query = _dbSet;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return orderBy(query).AsNoTracking().ToList();
        }

        return query.AsNoTracking().ToList();
    }

    public T Insert(T entity)
    {
        _dbSet.Add(entity);

        return entity;
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public T Update(T entity)
    {
        _dbSet.Update(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}