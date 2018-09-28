using HomeManager.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManager.Common.Repository
{
    public interface IDBContext
    {
        Task<IEnumerable<ISensorReading<double>>> QueryTemperature(Dictionary<string, object> queryParameters);
        Task<bool> AddTemperature(ISensorReading<double> reading);

        Task<IEnumerable<ISensorReading<double>>> QueryHumidity(Dictionary<string, object> queryParameters);
        Task<bool> AddHumidity(ISensorReading<double> reading);

        Task<IEnumerable<ISensor>> QuerySensors(Dictionary<string, object> queryParameters);
        Task<bool> AddSensor(ISensor sensor);
    }
}
