using System;
using System.Threading.Tasks;
using Common.SensorServiceAPI;
using Newtonsoft.Json;
using SensorListener.Model;

namespace SensorListener.Listeners
{
    public class SensorReadingListener: ISensorListener
    {
        public string Topic { get; }
        public string SensorServiceURL { get; }
        protected SensorServiceAPI Client { get; }

        public SensorReadingListener(string topic, string sensorServiceURL)
        {
            this.Topic = topic;
            this.SensorServiceURL = sensorServiceURL;
            this.Client = new SensorServiceAPI(this.SensorServiceURL);
        }

        public async Task<bool> ProcessMessageAsync(string message)
        {
            try
            {
                var reading = JsonConvert.DeserializeObject<QueueMessage>(message);
                reading.Environment.Timestamp = DateTime.UtcNow;

                var result = await this.Client.CreateEnvironmentReading(reading.SensorId, reading.Environment);

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
