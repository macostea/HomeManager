using Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace SensorListener.Model
{
    class EnvironmentMessage
    {
        public int SensorId { get; set; }
        public Environment Environment { get; set; }
    }
}
