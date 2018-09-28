using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeManager.Common.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T obj);
    }
}
