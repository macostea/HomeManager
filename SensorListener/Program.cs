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

            IQueueClient client = new RabbitMQClient(Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_EXCHANGE") ?? "SensorsExchange",
                                                     Environment.GetEnvironmentVariable("RABBITMQ_QUEUE") ?? "SensorsQueue");
            client.RegisterListener(new TemperatureSensorListener(Environment.GetEnvironmentVariable("SENSOR_SERVICE_URL") ?? "localhost"));

            client.Start();
        }
    }
}
