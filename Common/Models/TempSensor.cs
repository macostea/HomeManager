using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using HomeManager.Common.Repository;

namespace HomeManager.Common.Models
{
    public class TempSensor : ISensor<double>
    {
        public String Location { get; }
        private readonly List<ISensorReading<double>> readings;
        private readonly IRepository<ISensorReading<double>> repository;

        public TempSensor(String location, IRepository<ISensorReading<double>> repository)
        {
            this.Location = location;
            this.readings = new List<ISensorReading<double>>();
            this.repository = repository;
        }

        public IEnumerable<ISensorReading<double>> GetLatestReadings()
        {
            DateTime start = DateTime.Now.Subtract(TimeSpan.FromDays(3));
            DateTime end = DateTime.Now;
            return this.GetReadings((obj) => obj.Time > start && obj.Time < end);
        }

        public IEnumerable<ISensorReading<double>> GetReadings()
        {
            return this.GetReadings(null);
        }

        public IEnumerable<ISensorReading<double>> GetReadings(Predicate<ISensorReading<double>> filter)
        {
            return this.repository.GetList(this.Location, filter);
        }

        public Task<bool> SaveReading(ISensorReading<double> reading)
        {
            var task = new TaskFactory<bool>().StartNew(() =>
            {
                return this.repository.Add(reading);
            });

            return task;
        }
    }
}
