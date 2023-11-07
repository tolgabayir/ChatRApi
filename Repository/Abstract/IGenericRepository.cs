using System;
using System.Linq.Expressions;
using SignalR101.Helpers;
using SignalR101.Models;

namespace SignalR101.Repository.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        ResponseModel<T> Get(string id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        Task<ResponseModel<T>> AddAsync(T entity);
        ResponseModel<T> Delete(T entity);
        ResponseModel<T> DeleteAll(List<T> values);
        void Edit(T entity);
        void Save();
    }
}

