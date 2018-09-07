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
        IEnumerable<ISensorReading<double>> QueryTemperature(Dictionary<string, object> queryParameters);
        bool AddTemperature(ISensorReading<double> reading);

        IEnumerable<ISensorReading<double>> QueryHumidity(Dictionary<string, object> queryParameters);
        bool AddHumidity(ISensorReading<double> reading);

        IEnumerable<ISensor> QuerySensors(Dictionary<string, object> queryParameters);
        bool AddSensor(ISensor sensor);
    }
}
