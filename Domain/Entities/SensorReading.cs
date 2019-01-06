using System;
using System.Data.Common;

namespace Domain.Entities
{
    public abstract class ScalarSensorReading<T> : EntityBase
    {
        public int SensorReadingId { get; set; }

        public DateTime Time { get; set; }
        public T Reading { get; set; }

        public Sensor Sensor { get; set; }
        public int SensorId { get; set; }

        protected ScalarSensorReading() {}

        protected ScalarSensorReading(DateTime time, T reading)
        {
            this.Time = time;
            this.Reading = reading;
        }

        public override string ToString()
        {
            return $"Time: {this.Time}, Reading: {this.Reading}";
        }
    }
}
