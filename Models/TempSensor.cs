using System;
using System.Collections.Generic;
using HomeManager.Repository;

namespace HomeManager.Models
{
    public class TempSensor : ISensor
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

        public List<ISensorReading<double>> GetLatestReadings()
        {
            DateTime start = DateTime.Now.Subtract(TimeSpan.FromDays(3));
            DateTime end = DateTime.Now;
            return this.GetReadings((obj) => obj.Time > start && obj.Time < end);
        }

        public List<ISensorReading<double>> GetReadings()
        {
            return this.GetReadings(null);
        }

        public List<ISensorReading<double>> GetReadings(Predicate<ISensorReading<double>> filter)
        {
            return this.repository.GetList(this.Location, filter);
        }
    }
}
