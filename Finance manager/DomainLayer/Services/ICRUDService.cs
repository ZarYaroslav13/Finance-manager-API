﻿namespace DomainLayer.Services;

public interface ICRUDService<T_Domain, T_DB>
    where T_Domain : Models.Base.Entity
    where T_DB : DataLayer.Models.Base.Entity
{
    List<T_Domain> GetAll();

    T_Domain Add(T_Domain entity);

    T_Domain Find(int id);

    T_Domain Update(T_Domain entity);

    void Delete(T_Domain entity);
}
