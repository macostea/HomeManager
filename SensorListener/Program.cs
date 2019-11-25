using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SensorListener;
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

            var sensorServiceURL = Environment.GetEnvironmentVariable("SENSOR_SERVICE_URL") ?? "localhost";

            client.RegisterListener(new SensorReadingListener("environment", sensorServiceURL));
            client.RegisterListener(new WeatherReadingListener("weather", sensorServiceURL));
            client.RegisterListener(new NewSensorListener("sensor", sensorServiceURL));
            
            client.Start();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
