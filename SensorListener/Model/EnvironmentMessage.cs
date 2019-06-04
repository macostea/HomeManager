using Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace SensorListener.Model
{
    class EnvironmentMessage
    {
        public string SensorId { get; set; }
        public Environment Environment { get; set; }
    }
}
