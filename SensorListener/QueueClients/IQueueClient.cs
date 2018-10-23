using SensorListener.Listeners;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorListener.QueueClients
{
    interface IQueueClient
    {
        void RegisterListener(ISensorListener listener);
        void Start();
        void Stop();
    }
}
