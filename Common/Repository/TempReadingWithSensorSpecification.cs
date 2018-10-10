using System;
using Common.Models;

namespace Common.Repository
{
    public class TempReadingWithSensorSpecification : BaseSpecification<TemperatureSensorReading>
    {
        public TempReadingWithSensorSpecification(int readingId) : base(b => b.SensorReadingId == readingId)
        {
            AddInclude(b => b.Sensor);
        }
        public TempReadingWithSensorSpecification() : base(null)
        {
            AddInclude(b => b.Sensor);
        }
    }
}
