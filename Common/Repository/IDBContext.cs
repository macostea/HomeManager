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
        List<ISensorReading<double>> QueryTemperature(Dictionary<string, string> queryParameters);
        bool AddTemperature(ISensorReading<double> reading);
    }
}
