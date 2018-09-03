using HomeManager.SensorListener.Listeners;
using HomeManager.SensorListener.QueueClients;
using System;

namespace HomeManager.SensorListener
{
    class Program
    {
        static void Main(string[] args)
        {
            IQueueClient client = new RabbitMQClient(Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
                                                     Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE"),
                                                     Environment.GetEnvironmentVariable("RABBITMQ_QUEUE"));
            client.RegisterListener(new TemperatureSensorListener("temperature"));

            client.Start();
        }
    }
}
