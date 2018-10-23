using System;
using System.Threading.Tasks;
using Common.Models;
using Common.SensorServiceAPI;
using Newtonsoft.Json;

namespace SensorListener.Listeners
{
    public abstract class SensorReadingListener: ISensorListener
    {
        public string Topic { get; }
        public string SensorServiceURL { get; }
        protected SensorServiceAPI Client { get; }

        protected SensorReadingListener(string topic, string sensorServiceURL)
        {
            this.Topic = topic;
            this.SensorServiceURL = sensorServiceURL;
            this.Client = new SensorServiceAPI(this.SensorServiceURL);
        }

        public abstract Task<bool> ProcessMessageAsync(string message);
    }
}
