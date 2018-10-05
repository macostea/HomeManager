using System;
using Common.Models;

namespace Common.Repository
{
    public class HumidityReadingWithSensorSpecification : BaseSpecification<HumiditySensorReading>
    {
        public HumidityReadingWithSensorSpecification(int readingId) : base(b => b.SensorId == readingId)
        {
            AddInclude(b => b.Sensor);
        }

        public HumidityReadingWithSensorSpecification() : base(null)
        {
            AddInclude(b => b.Sensor);
        }
    }
}
