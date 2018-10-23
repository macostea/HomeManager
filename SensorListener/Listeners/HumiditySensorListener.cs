using Common.Models;
using Common.SensorServiceAPI;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SensorListener.Listeners
{
    class HumiditySensorListener : SensorReadingListener
    {
        public HumiditySensorListener(string sensorServiceURL) : base("humidity", sensorServiceURL) {}

        public override async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var reading = JsonConvert.DeserializeObject<HumiditySensorReading>(message);
                var result = await this.Client.CreateHumidityReading(reading);
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
