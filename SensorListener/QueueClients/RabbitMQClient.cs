using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SensorListener.Listeners;

namespace SensorListener.QueueClients
{
    public class RabbitMQClient : BackgroundService, IQueueClient
    {
        private readonly Dictionary<string, ISensorListener> listeners = new Dictionary<string, ISensorListener>();
        private readonly string hostname;
        private readonly string exchangeName;
        private readonly string queueName;
        private readonly string username;
        private readonly string password;

        private IConnection connection;
        private IModel listenChannel;
        private EventingBasicConsumer consumer;

        public RabbitMQClient(string hostname, string username, string password, string exchangeName, string queueName)
        {
            this.hostname = hostname;
            this.username = username;
            this.password = password;
            this.exchangeName = exchangeName;
            this.queueName = queueName;
        }

        public void RegisterListener(ISensorListener listener)
        {
            this.listeners.Add(listener.Topic, listener);
        }

        public void StartListening()
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.hostname,
                UserName = this.username,
                Password = this.password
            };

            this.connection = factory.CreateConnection();
            this.listenChannel = connection.CreateModel();

            listenChannel.ExchangeDeclare(exchange: exchangeName, type: "topic", durable: false);

            listenChannel.QueueDeclare(queue: this.queueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            foreach (var listener in this.listeners)
            {
                listenChannel.QueueBind(queueName, exchangeName, listener.Key);
            }

            this.consumer = new EventingBasicConsumer(listenChannel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    var listenerFound = this.listeners.TryGetValue(ea.RoutingKey, out ISensorListener listener);

                    if (listenerFound)
                    {
                        listener.ProcessMessageAsync(message);
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            };

            listenChannel.BasicConsume(queue: queueName,
                                autoAck: true,
                                consumer: consumer);
        }

        public Task PublishAsync(string topic, string message)
        {
            if (this.connection == null || !this.connection.IsOpen)
            {
                return Task.FromException(new Exception("Cannot publish. RabbitMQ Client not connected!"));
            }

            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(this.exchangeName, topic, null, body);
            }

            return Task.CompletedTask;
        }

        public void Stop()
        {
            this.consumer = null;
            this.listenChannel.Close();
            this.connection.Close();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(this.StartListening);
        }
    }
}
