using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Utils;
using Domain.Entities;
using RestSharp;
using Environment = Domain.Entities.Environment;

namespace Common.SensorListenerAPI
{
    public class SensorListenerAPI : ISensorListenerAPI
    {
        public Uri BaseURL { get; }

        public SensorListenerAPI(string baseURL)
        {
            this.BaseURL = new Uri(baseURL);
        }

        public async Task<Sensor> NotifySensorUpdate(Sensor sensor)
        {
            var sensorRequest = new RestRequest
            {
                Resource = "api/sensors"
            };
            sensorRequest.Method = Method.POST;
            sensorRequest.AddJsonBody(sensor);

            return await Utilities.ExecuteRestRequest<Sensor>(this.BaseURL, sensorRequest);
        }

        public async Task NotifyHomeyTopic<T>(string topic, T message)
        {
            var sensorRequest = new RestRequest
            {
                Resource = "api/sensors/homey/{topic}"
            };
            sensorRequest.Method = Method.POST;
            sensorRequest.AddUrlSegment("topic", topic);
            sensorRequest.AddParameter("message", message);
            
            await Utilities.ExecuteRestRequest(this.BaseURL, sensorRequest);
        }
    }
}
