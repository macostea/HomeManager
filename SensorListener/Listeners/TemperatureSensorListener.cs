using Common.Models;
using Common.SensorServiceAPI;
using Newtonsoft.Json;
using RestSharp;
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
            var reading = JsonConvert.DeserializeObject<TemperatureSensorReading>(message);

            var client = new SensorServiceAPI(this.SensorServiceURL);
            var result = await client.CreateTemperatureReading(reading);
        }
    }
}
