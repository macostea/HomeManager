using System;
using System.Collections;
using System.Data.Common;

namespace Domain.Entities
{
    public enum SensorType
    {
        Temperature,
        Humidity,
        Weather
    }

    public class Sensor : EntityBase
    {
        public int SensorId { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public SensorType Type { get; set; }
    }
}
