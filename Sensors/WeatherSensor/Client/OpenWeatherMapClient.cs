using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Common.Utils;
using RestSharp;

namespace WeatherSensor.Client
{
    public struct OpenWeatherMapResponseObject
    {
        public List<Dictionary<string, object>> Weather { get; set; }
        public Dictionary<string, object> Main { get; set; }
    }

    class OpenWeatherMapClient : IWeatherClient
    {
        public Uri BaseURL { get; }
        public string APIVersion { get; }
        public string APIKey { get; }
        public string CityName { get; }
        public OpenWeatherMapClient(string cityName, string apiKey)
        {
            this.BaseURL = new Uri("https://api.openweathermap.org/data");
            this.APIVersion = "2.5";
            this.CityName = cityName;
            this.APIKey = apiKey;
        }

        public async Task<Weather> GetCurrentConditions()
        {
            var request = new RestRequest
            {
                Resource = $"{this.APIVersion}/weather"
            };

            request.AddParameter("q", this.CityName);
            request.AddParameter("appid", this.APIKey);
            request.AddParameter("units", "metric");
            
            request.Method = Method.GET;

            var conditions = await Utilities.ExecuteRestRequest<OpenWeatherMapResponseObject>(this.BaseURL, request);
            var sensorReading = new Weather()
            {
                Temperature = Convert.ToDouble(conditions.Main["temp"]),
                Pressure = Convert.ToDouble(conditions.Main["pressure"]),
                Humidity = Convert.ToDouble(conditions.Main["humidity"]),
                MinimumTemperature = Convert.ToDouble(conditions.Main["temp_min"]),
                MaximumTemperature = Convert.ToDouble(conditions.Main["temp_max"]),
                ConditionCode = Convert.ToInt32(conditions.Weather[0]["id"]),
                Condition = (string)conditions.Weather[0]["main"],
                IconURL = new Uri(new Uri("http://openweathermap.org/img/w/"), (string)conditions.Weather[0]["icon"])
            };

            return sensorReading;
        }
    }
}
