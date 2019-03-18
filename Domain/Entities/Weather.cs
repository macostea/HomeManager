using System;

namespace Domain.Entities
{
    public class Weather : EntityBase
    {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double MinimumTemperature { get; set; }
        public double MaximumTemperature { get; set; }
        public int ConditionCode { get; set; }
        public string Condition { get; set; }
        public Uri IconURL { get; set; }
    }
}
