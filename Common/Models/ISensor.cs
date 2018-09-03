using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeManager.Common.Models
{
    public interface ISensor<T>
    {
        IEnumerable<ISensorReading<T>> GetLatestReadings();
        IEnumerable<ISensorReading<T>> GetReadings();
        IEnumerable<ISensorReading<T>> GetReadings(Predicate<ISensorReading<T>> filter);

        Task<bool> SaveReading(ISensorReading<T> reading);
    }
}
