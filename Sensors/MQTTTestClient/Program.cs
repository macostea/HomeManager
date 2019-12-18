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
        enum States { New, WaitingResponse, Registered }

        static States State;

        static async Task Main(string[] args)
        {
            State = States.New;
            string roomId = null;
            var id = "B2904CCF-4B7D-4457-A26C-6CBCA89EF02E".ToLower();

            var configuration = new MqttConfiguration();
            var client = await MqttClient.CreateAsync("localhost", configuration);
            var credentials = new MqttClientCredentials(null, "rabbit", "rabbit");
            var sessionState = await client.ConnectAsync(credentials, null, true);

            await client.SubscribeAsync(id, MqttQualityOfService.AtLeastOnce);

            client.MessageStream.Subscribe(msg =>
            {
                if (msg.Topic == id)
                {
                    if (State == States.WaitingResponse)
                    {
                        var msgObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(msg.Payload));
                        roomId = (string)msgObj["RoomId"]; // Not used yet as we can get the roomId from SensorService based on sensorId
                        State = States.Registered;
                    }
                }
            });

            while (true)
            {
                switch (State)
                {
                    case States.New:
                        await PublishNewSensorMessage(client, id).ConfigureAwait(false);
                        State = States.WaitingResponse;
                        break;

                    case States.WaitingResponse:
                        break;

                    case States.Registered:
                        await PublishEnvironment(client, id).ConfigureAwait(false);
                        break;

                    default:
                        throw new NotImplementedException("Invalid state");
                        
                }
                await Task.Delay(200).ConfigureAwait(false);
            }
        }

        static async Task PublishNewSensorMessage(IMqttClient client, string id)
        {
            var sensorObj = new Dictionary<string, object>()
            {
                { "sensor", new Dictionary<string, object>
                    {
                        { "id", id },
                        { "type", "test" }
                    }
                }
            };
            var message = new MqttApplicationMessage("sensor", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(sensorObj)));
            await client.PublishAsync(message, MqttQualityOfService.ExactlyOnce);
        }

        static async Task PublishEnvironment(IMqttClient client, string id)
        {
            Random rnd = new Random();
            var env = new Dictionary<string, object>
            {
                { "sensorId", id},
                { "environment", new Dictionary<string, object>
                    {
                        { "temperature", rnd.Next(20, 35) },
                        { "humidity", rnd.Next(20, 60) }
                    }
                }
            };

            var message = new MqttApplicationMessage("environment", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(env)));
            await client.PublishAsync(message, MqttQualityOfService.AtMostOnce);

            await Task.Delay(TimeSpan.FromMinutes(5)).ConfigureAwait(false);
        }
    }
}
