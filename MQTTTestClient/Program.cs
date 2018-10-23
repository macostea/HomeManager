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

            var tempReading = new Dictionary<string, object>
            {
                { "sensorId", 2 },
                { "time", "2018-10-23T13:14:17Z"},
                { "reading", 21.0 }
            };

            var message = new MqttApplicationMessage("temperature", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tempReading)));
            await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await client.DisconnectAsync();
        }
    }
}
