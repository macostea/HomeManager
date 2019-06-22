using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            // TODO: Location could be received from SensorService via Home ID
            var client = new OpenWeatherMapClient(
                Environment.GetEnvironmentVariable("WEATHER_LOCATION"),
                openweathermapAppid
            );
            var response = await client.GetCurrentConditions();

            var messageDict = new Dictionary<string, object>
            {
                ["homeId"] = Convert.ToInt32(Environment.GetEnvironmentVariable("HOME_ID")),
                ["weather"] = response
            };

            var configuration = new MqttConfiguration();
            var rabbitmqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            var mqttClient = await MqttClient.CreateAsync(rabbitmqHost, configuration);

            var clientId = Guid.NewGuid().ToString();
            var username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME");
            var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD");

            MqttClientCredentials credentials;

            if (username == null)
            {
                credentials = new MqttClientCredentials(clientId);
            }
            else
            {
                credentials = new MqttClientCredentials(clientId, username, password);
            }

            var sessionState = await mqttClient.ConnectAsync(credentials);

            var message = new MqttApplicationMessage("weather", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageDict)));
            await mqttClient.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await mqttClient.DisconnectAsync();
        }
    }
}
