using Common.Models;
using Common.SensorServiceAPI;
using Newtonsoft.Json;
using RestSharp;
using SensorListener.Listeners;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SensorListener.Listeners
{
    class TemperatureSensorListener : SensorReadingListener
    {
        public TemperatureSensorListener(string sensorServiceURL) : base("temperature", sensorServiceURL) {}

        public override async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var reading = JsonConvert.DeserializeObject<TemperatureSensorReading>(message);
                reading.Time = DateTime.UtcNow;

                var result = await this.Client.CreateTemperatureReading(reading);

                Console.WriteLine(result);
                return true;
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
