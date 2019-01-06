using Domain.Entities;
using Newtonsoft.Json;
using System;
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
