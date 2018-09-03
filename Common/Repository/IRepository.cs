using System;
using System.Collections.Generic;

namespace HomeManager.Common.Repository
{
    public interface IRepository<T>
    {
        List<T> GetList(string room, Predicate<T> filter);
    }
}
