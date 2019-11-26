using SensorListener.Listeners;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SensorListener.QueueClients
{
    interface IQueueClient
    {
        void RegisterListener(ISensorListener listener);
        Task PublishAsync(string topic, string message);
        void StartListening();
        void Stop();
    }
}
