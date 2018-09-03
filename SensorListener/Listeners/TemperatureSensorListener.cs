using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common.Models;

namespace HomeManager.SensorListener.Listeners
{
    class TemperatureSensorListener : ISensorListener
    {
        public string Topic { get; }
        private ISensor<double> Sensor { get; }

        public TemperatureSensorListener(ISensor<double> sensor)
        {
            this.Topic = "temperature";
            this.Sensor = sensor;
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
