using SensorListener.Listeners;
using SensorListener.QueueClients;
using System;

namespace HomeManager.SensorListener
{
    class Program
    {
        static void Main(string[] args)
        {
            IQueueClient client = new RabbitMQClient(Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE") ?? "SensorsExchange",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? "SensorsQueue");
            client.RegisterListener(new SensorReadingListener("environment", Environment.GetEnvironmentVariable("SENSOR_SERVICE_URL") ?? "localhost"));
            client.RegisterListener(new WeatherReadingListener("weather", Environment.GetEnvironmentVariable("SENSOR_SERVICE_URL") ?? "localhost"));
            
            client.Start();
        }
    }
}
