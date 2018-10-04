using System;
using System.Data.Common;

namespace Common.Models
{
    public class Sensor : EntityBase
    {
        public String Name { get; set; }
        public String Location { get; set; }
        public String Type { get; set; }
    }
}
