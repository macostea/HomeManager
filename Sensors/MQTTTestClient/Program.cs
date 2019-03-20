using System;
using System.Collections.Generic;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MQTTTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new MqttConfiguration();
            var client = await MqttClient.CreateAsync("foxey-lady-master.mcostea.com", configuration);
            var sessionState = await client.ConnectAsync();

            var env = new Dictionary<string, object>
            {
                { "sensorId", 3 },
                { "environment", new Dictionary<string, object>
                    {
                        { "temperature", 22 },
                        { "humidity", 32 }
                    }
                }
            };

            var message = new MqttApplicationMessage("environment", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(env)));
            await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await client.DisconnectAsync();
        }
    }
}
