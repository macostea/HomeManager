using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SensorListener.Listeners
{
    class WeatherSensorListener : SensorReadingListener
    {
        public WeatherSensorListener(string sensorServiceURL) : base("weather", sensorServiceURL) {}

        public override async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var reading = JsonConvert.DeserializeObject<WeatherSensorReading>(message);
                reading.Time = DateTime.UtcNow;

                var result = await this.Client.CreateWeatherReading(reading);

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
