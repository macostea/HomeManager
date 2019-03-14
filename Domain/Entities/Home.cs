using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Home : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
