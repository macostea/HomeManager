using System;
using System.Data.Common;

namespace HomeManagerWeb.Models
{
    public class SensorReading<T> : ISensorReading<T>
    {
        public DateTime Time { get; }
        public T Reading { get; }

        public SensorReading(DateTime time, T reading)
        {
            this.Time = time;
            this.Reading = reading;
        }
    }
}
