using System;
using System.Collections.Generic;
using HomeManagerWeb.Models;

namespace HomeManagerWeb.Repository
{
    public class TempSensorReadingsRepository : IRepository<ISensorReading<double>>
    {
        public TempSensorReadingsRepository()
        {
        }

        public List<ISensorReading<double>> GetList(Predicate<ISensorReading<double>> filter)
        {
            var readings = new List<ISensorReading<double>>
            {
                new SensorReading<double>(DateTime.Now, 24.4),
                new SensorReading<double>(DateTime.Now, 24.6),
                new SensorReading<double>(DateTime.Now, 23.2)
            };

            return null == filter ? readings : readings.FindAll(filter);
        }
    }
}
