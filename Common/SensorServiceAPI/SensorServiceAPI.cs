using Domain.Entities;
using Common.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Environment = Domain.Entities.Environment;

namespace Common.SensorServiceAPI
{
    public class SensorServiceAPI
    {
        public Uri BaseURL { get; }
        public SensorServiceAPI(string baseURL)
        {
            this.BaseURL = new Uri(baseURL);
        }

        public async Task<Environment> CreateEnvironmentReading(string sensorId, Environment environment)
        {
            var roomRequest = new RestRequest
            {
                Resource = "api/rooms"
            };
            roomRequest.Method = Method.GET;
            roomRequest.AddParameter("sensorId", sensorId);

            var room = await Utilities.ExecuteRestRequest<Room>(this.BaseURL, roomRequest);

            var envRequest = new RestRequest
            {
                Resource = "api/rooms/{roomId}/sensor/{sensorId}/environment"
            };
            envRequest.AddUrlSegment("roomId", room.Id);
            envRequest.AddUrlSegment("sensorId", sensorId);
            envRequest.Method = Method.POST;
            envRequest.AddJsonBody(environment);

            return await Utilities.ExecuteRestRequest<Environment>(this.BaseURL, envRequest);
        }

        public async Task<Weather> CreateWeatherReading(string homeId, Weather weather)
        {
            var weatherRequest = new RestRequest
            {
                Resource = "api/homes/{homeId}/weather"
            };
            weatherRequest.AddUrlSegment("homeId", homeId);
            weatherRequest.Method = Method.POST;
            weatherRequest.AddJsonBody(weather);

            return await Utilities.ExecuteRestRequest<Weather>(this.BaseURL, weatherRequest);
        }

        public async Task<Sensor> CreateSensor(Sensor sensor)
        {
            var newSensorRequest = new RestRequest
            {
                Resource = "api/sensors"
            };
            newSensorRequest.Method = Method.POST;
            newSensorRequest.AddJsonBody(sensor);

            return await Utilities.ExecuteRestRequest<Sensor>(this.BaseURL, newSensorRequest);
        }

        public async Task<List<Weather>> GetWeather(string homeId, DateTime startDate, DateTime endDate)
        {
            var weatherRequest = new RestRequest
            {
                Resource = "api/homes/{homeId}/weather"
            };
            weatherRequest.AddUrlSegment("homeId", homeId);
            weatherRequest.AddParameter("startDate", startDate);
            weatherRequest.AddParameter("endDate", endDate);

            return await Utilities.ExecuteRestRequest<List<Weather>>(this.BaseURL, weatherRequest);
        }
    }
}
