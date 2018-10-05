using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Common.Models
{
    public abstract class SensorReading<T> : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensorReadingId { get; set; }

        public DateTime Time { get; set; }
        public T Reading { get; set; }

        public Sensor Sensor { get; set; }
        public int SensorId { get; set; }

        protected SensorReading() {}

        protected SensorReading(DateTime time, T reading)
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
