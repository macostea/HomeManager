using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.SensorListener.Listeners
{
    class TemperatureSensorListener : ISensorListener
    {
        public string Topic { get; }

        public TemperatureSensorListener(string topic)
        {
            this.Topic = topic;
        }

        public void ProcessMessageAsync(string message)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"{this.Topic}: {message}");
            });
        }
    }
}
