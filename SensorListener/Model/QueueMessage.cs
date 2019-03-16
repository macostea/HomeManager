using Domain.Entities;
using System.Collections.Generic;
using System.Text;

namespace SensorListener.Model
{
    class QueueMessage
    {
        public int SensorId { get; set; }
        public Environment Environment { get; set; }
    }
}
