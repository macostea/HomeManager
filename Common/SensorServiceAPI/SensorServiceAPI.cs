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

        public async Task<Environment> CreateEnvironmentReading(int sensorId, Environment environment)
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

        //    public async Task<TemperatureSensorReading> CreateTemperatureReading(TemperatureSensorReading reading)
        //    {
        //        var request = new RestRequest
        //        {
        //            Resource = "api/TemperatureSensorReadings"
        //        };
        //        request.AddJsonBody(reading);
        //        request.Method = Method.POST;

        //        return await Utilities.ExecuteRestRequest<TemperatureSensorReading>(this.BaseURL, request);
        //    }

        //    public async Task<HumiditySensorReading> CreateHumidityReading(HumiditySensorReading reading)
        //    {
        //        var request = new RestRequest
        //        {
        //            Resource = "api/HumiditySensorReadings"
        //        };
        //        request.AddJsonBody(reading);
        //        request.Method = Method.POST;

        //        return await Utilities.ExecuteRestRequest<HumiditySensorReading>(this.BaseURL, request);
        //    }

        //    public async Task<WeatherSensorReading> CreateWeatherReading(WeatherSensorReading reading)
        //    {
        //        var request = new RestRequest
        //        {
        //            Resource = "api/WeatherSensorReadings"
        //        };
        //        request.AddJsonBody(reading);
        //        request.Method = Method.POST;

        //        return await Utilities.ExecuteRestRequest<WeatherSensorReading>(this.BaseURL, request);
        //    }
    }
}
