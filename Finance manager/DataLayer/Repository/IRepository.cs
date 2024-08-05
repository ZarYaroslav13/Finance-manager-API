using DataLayer.Models.Base;
using System.Linq.Expressions;

namespace DataLayer.Repository;

public interface IRepository<T> where T : Entity
{
    IEnumerable<T> GetAll(
            Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null,
                Expression<Func<T, bool>> filter = null,
                params string[] includeProperties);

    T Insert(T entity);

    T GetById(int id);

    T Update(T entity);

    void Delete(int id);
}