using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.SensorListener.Listeners
{
    class TemperatureSensorListener : ISensorListener
    {
        public string Topic { get; }
        public string SensorServiceURL { get; }

        public TemperatureSensorListener(string sensorServiceURL)
        {
            this.Topic = "temperature";
            this.SensorServiceURL = sensorServiceURL;
        }

        public async void ProcessMessageAsync(string message)
        {
            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"{this.Topic}: {message}");
            });
        }
    }
}
