using System;
namespace Domain.Entities
{
    public class Environment : EntityBase
    {
        public DateTime Timestamp { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public bool Motion { get; set; }
    }
}
