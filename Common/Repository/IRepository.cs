using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<bool> Add(T obj);
        Task<bool> Delete(T entity);
        Task<bool> Edit(T entity);
    }
}
