using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models
{
    public class TemperatureSensorReading : SensorReading<double>
    {
        [ForeignKey("TempSensorForeignKey")]
        public Sensor Sensor;
        public int SensorID;

        public TemperatureSensorReading() : base()
        {
        }
    }
}
