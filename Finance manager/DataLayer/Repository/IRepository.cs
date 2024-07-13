﻿using DataLayer.Models.Base;

namespace DataLayer.Repository;

public interface IRepository<T> where T : Entity
{
    IEnumerable<T> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties);

    void Insert(T entity);

    T GetById(int id);

    void Update(T entity);

    void Delete(T entity);
}