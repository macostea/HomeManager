using System;
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
    }
}
