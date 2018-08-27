using HomeManagerWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace HomeManagerWeb.Repository
{
    public interface IDBContext
    {
        List<ISensorReading<double>> QueryTemperature(Dictionary<string, string> queryParameters);
    }
}
