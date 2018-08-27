using System;
using System.Collections.Generic;

namespace HomeManagerWeb.Repository
{
    public interface IRepository<T>
    {
        List<T> GetList(Predicate<T> filter);
    }
}
