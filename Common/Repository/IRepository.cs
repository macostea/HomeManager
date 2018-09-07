using System;
using System.Collections.Generic;

namespace HomeManager.Common.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        bool Add(T obj);
    }
}
