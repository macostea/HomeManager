using System;
using System.Threading.Tasks;
using Common.SensorServiceAPI;
using Newtonsoft.Json;
using SensorListener.Model;

namespace SensorListener.Listeners
{
    public class WeatherReadingListener: ISensorListener
    {
        public string Topic { get; }
        public string SensorServiceURL { get; }
        protected SensorServiceAPI Client { get; }

        public WeatherReadingListener(string topic, string sensorServiceURL)
        {
            this.Topic = topic;
            this.SensorServiceURL = sensorServiceURL;
            this.Client = new SensorServiceAPI(this.SensorServiceURL);
        }

        public async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var reading = JsonConvert.DeserializeObject<WeatherMessage>(message);
                reading.Weather.Timestamp = DateTime.UtcNow;
                
                var result = await this.Client.CreateWeatherReading(reading.HomeId, reading.Weather);

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
