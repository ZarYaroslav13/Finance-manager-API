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

    public virtual async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null,
                Expression<Func<T, bool>> filter = null,
                int skip = 0,
                int take = 0,
                params string[] includeProperties)
    {
        if (skip < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(skip));
        }

        if (take < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(take));
        }

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
            query = orderBy(query);
        }

        if (skip != 0)
        {
            query = query.Skip(skip);
        }

        if (take != 0)
        {
            query = query.Take(take);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public T Insert(T entity)
    {
        _dbSet.Add(entity);

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var toUpdate = await _dbSet.FindAsync(entity.Id);

        if (toUpdate != null)
        {
            toUpdate = entity;
        }

        _dbSet.Update(toUpdate);

        return entity;
    }

    public void Delete(int id)
    {
        var entity = _dbSet.Find(id);

        _dbSet.Remove(entity);
    }
}