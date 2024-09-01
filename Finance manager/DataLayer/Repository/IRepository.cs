using DataLayer.Models.Base;
using System.Linq.Expressions;

namespace DataLayer.Repository;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null,
                Expression<Func<T, bool>> filter = null,
                int skip = 0,
                int take = 0,
                params string[] includeProperties);

    Task<T> GetByIdAsync(int id);

    T Insert(T entity);

    Task<T> UpdateAsync(T entity);

    void Delete(int id);
}