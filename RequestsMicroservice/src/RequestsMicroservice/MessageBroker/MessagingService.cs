using Newtonsoft.Json;
using RabbitMQ.Client;
using RequestsMicroservice.Contracts.Messaging;
using System;
using System.Text;

namespace RequestsMicroservice.MessageBroker
{
    public class MessagingService : IMessagingService
    {
        private readonly ConnectionFactory _connectionFactory;
        
        public MessagingService(IConnectionSettings settings)
        {
            _connectionFactory = new ConnectionFactory { HostName = settings.Hostname };

            if (settings.Port != null)
            {
                _connectionFactory.Port = settings.Port.Value;
            }
        }

        public void Send<TMessage>(TMessage message)
        {
            var serializedMessage = JsonConvert.SerializeObject(message);

            SendInternal(serializedMessage);
        }

        protected void SendInternal(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueDefinition.QueueName,
                        durable: QueueDefinition.IsDurable,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    channel.BasicPublish(exchange: string.Empty, 
                        routingKey: QueueDefinition.QueueName,
                        body: body);

                    Console.WriteLine($"[RabbitMQ] Sent '{message}'");
                }
            }
        }
    }
}