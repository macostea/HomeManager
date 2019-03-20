using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SensorListener.Listeners;

namespace SensorListener.QueueClients
{
    class RabbitMQClient : IQueueClient
    {
        private readonly Dictionary<string, ISensorListener> listeners = new Dictionary<string, ISensorListener>();
        private readonly string hostname;
        private readonly string exchangeName;
        private readonly string queueName;
        private readonly string username;
        private readonly string password;

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

        public void Start()
        {
            var factory = new ConnectionFactory()
            {
                HostName = this.hostname,
                UserName = this.username,
                Password = this.password
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic", durable: false);

                    channel.QueueDeclare(queue: this.queueName,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    foreach (var listener in this.listeners)
                    {
                        channel.QueueBind(queueName, exchangeName, listener.Key);
                    }

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        var listenerFound = this.listeners.TryGetValue(ea.RoutingKey, out ISensorListener listener);

                        if (listenerFound)
                        {
                            listener.ProcessMessageAsync(message);
                        }
                    };

                    channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

                    var autoResetEvent = new AutoResetEvent(false);
                    Console.CancelKeyPress += (sender, eventArgs) =>
                    {
                        eventArgs.Cancel = true;
                        autoResetEvent.Set();
                    };

                    autoResetEvent.WaitOne();
                }
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
