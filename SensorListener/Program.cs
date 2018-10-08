using HomeManager.SensorListener.Listeners;
using HomeManager.SensorListener.QueueClients;
using System;

namespace HomeManager.SensorListener
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dBContext = new TimescaleDBContext();
            //var repository = new TempSensorReadingsRepository(dBContext);
            //var sensor = new TempSensor("office", repository);

            //IQueueClient client = new RabbitMQClient(Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
            //                                         Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE"),
            //                                         Environment.GetEnvironmentVariable("RABBITMQ_QUEUE"));
            //client.RegisterListener(new TemperatureSensorListener(sensor));

            //client.Start();
        }
    }
}
