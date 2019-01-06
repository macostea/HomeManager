using Domain.Entities;
using Newtonsoft.Json;
using System;
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
                reading.Time = DateTime.UtcNow;

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
