using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Room : EntityBase
    {
        public string Name { get; set; }
        public Home Home { get; set; }
        public List<Sensor> Sensors { get; set; }
    }
}
