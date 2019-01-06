using Newtonsoft.Json;
using System;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using WeatherSensor.Client;

namespace WeatherSensor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var openweathermapAppid = Environment.GetEnvironmentVariable("OPENWEATHERMAP_APPID");
            if (openweathermapAppid == null)
            {
                openweathermapAppid = await System.IO.File.ReadAllTextAsync("/run/secrets/OPENWEATHERMAP_APPID");
            }

            var client = new OpenWeatherMapClient(
                Environment.GetEnvironmentVariable("WEATHER_LOCATION"),
                openweathermapAppid
            );
            var response = await client.GetCurrentConditions();
            response.SensorId = Convert.ToInt32(Environment.GetEnvironmentVariable("SENSOR_ID"));

            var configuration = new MqttConfiguration();
            var mqttClient = await MqttClient.CreateAsync(Environment.GetEnvironmentVariable("RABBITMQ_HOST"), configuration);
            var sessionState = await mqttClient.ConnectAsync();

            var message = new MqttApplicationMessage("weather", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
            await mqttClient.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await mqttClient.DisconnectAsync();
        }
    }
}
