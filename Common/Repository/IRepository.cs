using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Common.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll(ISpecification<T> spec);
        Task<bool> Add(T obj);
        Task<bool> Delete(T entity);
        Task<bool> Edit(T entity);
    }
}
