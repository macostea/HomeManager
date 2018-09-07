using System;
using System.Data.Common;

namespace HomeManager.Common.Models
{
    public class SensorReading<T> : ISensorReading<T>
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

        public void FromTimescaleReader(DbDataReader reader)
        {
            this.Time = reader.GetDateTime(0);
            this.Reading = (T)reader.GetValue(1);
        }
    }
}
