using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.SensorServiceAPI
{
    public class SensorServiceAPI
    {
        public string BaseURL { get; }
        public SensorServiceAPI(string baseURL)
        {
            this.BaseURL = baseURL;
        }

        public async Task<T> Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(BaseURL)
            };
            var response = await client.ExecuteTaskAsync<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

        public async Task<TemperatureSensorReading> CreateTemperatureReading(TemperatureSensorReading reading)
        {
            var request = new RestRequest
            {
                Resource = "api/TemperatureSensorReadings"
            };
            request.AddJsonBody(reading);
            request.Method = Method.POST;

            return await Execute<TemperatureSensorReading>(request);
        }

        public async Task<HumiditySensorReading> CreateHumidityReading(HumiditySensorReading reading)
        {
            var request = new RestRequest
            {
                Resource = "api/HumiditySensorReadings"
            };
            request.AddJsonBody(reading);
            request.Method = Method.POST;

            return await Execute<HumiditySensorReading>(request);
        }
    }
}
