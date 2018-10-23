using System;
using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;

namespace MQTTTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new MqttConfiguration();
            var client = await MqttClient.CreateAsync("10.230.5.60", configuration);
            var sessionState = await client.ConnectAsync();

            var message = new MqttApplicationMessage("donnowhat", Encoding.UTF8.GetBytes("Hello world"));
            await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await client.DisconnectAsync();
        }
    }
}
