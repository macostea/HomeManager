using System;
using System.Collections.Generic;
using HomeManagerWeb.Models;

namespace HomeManagerWeb.Repository
{
    public class TempSensorReadingsRepository : IRepository<ISensorReading<double>>
    {
        private readonly IDBContext db;

        public TempSensorReadingsRepository(IDBContext db)
        {
            this.db = db;
        }

        public List<ISensorReading<double>> GetList(Predicate<ISensorReading<double>> filter)
        {
            var readings = this.db.QueryTemperature("SELECT * FROM temperature");

            return null == filter ? readings : readings.FindAll(filter);
        }
    }
}
