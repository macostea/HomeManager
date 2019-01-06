using System;
using Domain.Entities;

namespace Common.Repository
{
    public class WeatherReadingWithSensorSpecification : BaseSpecification<WeatherSensorReading>
    {
        public int ReadingID { get; }

        public WeatherReadingWithSensorSpecification(int readingId) : base(b => b.SensorReadingId == readingId)
        {
            this.ReadingID = readingId;
            AddInclude(b => b.Sensor);
        }

        public WeatherReadingWithSensorSpecification() : base(null)
        {
            AddInclude(b => b.Sensor);
        }
    }
}
