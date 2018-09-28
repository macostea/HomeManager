using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Common.Models;

namespace HomeManager.Common.Repository
{
    public class TempSensorReadingsRepository : IRepository<ISensorReading<double>>
    {
        private readonly IDBContext db;
        private readonly ISensor sensor;

        public TempSensorReadingsRepository(IDBContext db, ISensor sensor)
        {
            this.db = db;
            this.sensor = sensor;
        }

        public Task<bool> Add(ISensorReading<double> obj)
        {
            return this.db.AddTemperature(obj);
        }

        public Task<IEnumerable<ISensorReading<double>>> GetAll()
        {

            var parameters = new Dictionary<string, object>
            {
                { "sensor", sensor.Id }
            };

            return this.db.QueryTemperature(parameters);
        }
    }
}
