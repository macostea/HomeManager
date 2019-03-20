using System;
using System.Collections;
using System.Data.Common;

namespace Domain.Entities
{
    public class Sensor : EntityBase
    {
        public String Name { get; set; }
        public String Type { get; set; }
    }
}
