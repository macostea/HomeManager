using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WeatherSensorReading : EntityBase
    {
        public int SensorReadingId { get; set; }
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public Nullable<double> Pressure { get; set; }
        public Nullable<double> Humidity { get; set; }
        public double MinimumTemperature { get; set; }
        public double MaximumTemperature { get; set; }
        public Nullable<int> ConditionCode { get; set; }
        public string Condition { get; set; }
        public Uri IconURL { get; set; }

        public Sensor Sensor { get; set; }
        public int SensorId { get; set; }

        public WeatherSensorReading() : base()
        {
        }

        public override string ToString()
        {
            return $"Time: {this.Time}, Temperature: {this.Temperature}, Condition: {this.Condition}";
        }
    }
}
