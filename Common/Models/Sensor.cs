using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace Common.Models
{
    public enum SensorType
    {
        Temperature,
        Humidity
    }

    public class Sensor : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SensorId { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public SensorType Type { get; set; }
    }
}
