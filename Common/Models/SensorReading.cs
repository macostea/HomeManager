using System;
using System.Data.Common;

namespace Common.Models
{
    public class SensorReading<T> : EntityBase
    {
        public DateTime Time { get; set; }
        public T Reading { get; set; }

        public SensorReading() { }

        public SensorReading(DateTime time, T reading)
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
