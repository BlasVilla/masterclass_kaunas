using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RequestsMicroservice.Contracts.Messaging;
using RequestsMicroservice.Contracts.Requests;
using ResultsMicroservice.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Worker.Calculations;
using Worker.Settings;

namespace Worker.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly ICalculationService _calculationService;
        
        private readonly IServiceSettings _serviceSettings;

        private readonly ISimulationSettings _simulationSettings;

        private readonly ConnectionFactory _connectionFactory;
        
        public MessageService(ICalculationService calculationService, 
            IServiceSettings serviceSettings, 
            IConnectionSettings connectionSettings, 
            ISimulationSettings simulationSettings)
        {
            _calculationService = calculationService;
            _serviceSettings = serviceSettings;
            _simulationSettings = simulationSettings;

            _connectionFactory = new ConnectionFactory { HostName = connectionSettings.Hostname };

            if (connectionSettings.Port != null)
            {
                _connectionFactory.Port = connectionSettings.Port.Value;
            }

            Console.WriteLine($"[{nameof(MessageService)}] Hostname: '{_connectionFactory.HostName}'. Port: '{_connectionFactory.Port}'");
        }

        public void Run()
        {
            Console.WriteLine($"[{nameof(MessageService)}] Creating connection...");

            using (var connection = _connectionFactory.CreateConnection())
            {
                Console.WriteLine($"[{nameof(MessageService)}] Creating channel...");

                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine($"[{nameof(MessageService)}] Declaring queue...");

                    channel.QueueDeclare(queue: QueueDefinition.QueueName,
                        durable: QueueDefinition.IsDurable,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    Console.WriteLine($"[{nameof(MessageService)}] Setup consume queue...");

                    var consumer = new EventingBasicConsumer(channel);

                    EventHandler<BasicDeliverEventArgs> delegateMethod = (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"[RabbitMQ] Received message '{message}'");

                        try
                        {
                            HandleMessage(message).GetAwaiter().GetResult();

                            Console.WriteLine($"Acking message. Delivery Tag: {ea.DeliveryTag}");

                            // ReSharper disable once AccessToDisposedClosure
                            channel.BasicAck(deliveryTag: ea.DeliveryTag,
                                multiple: false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR]: '{ex.Message}");
                        }
                    };

                    consumer.Received += delegateMethod;
                    
                    channel.BasicConsume(queue: QueueDefinition.QueueName, 
                        noAck: false, 
                        consumer: consumer);

                    Console.WriteLine("Press any key to stop...");

                    Console.ReadKey();
                }
            }
        }

        protected async Task HandleMessage(string message)
        {
            var newRequestMessage = JsonConvert.DeserializeObject<NewRequestMessage>(message);

            string entity;

            using (var client = new HttpClient())
            {
                var resourceUri = new Uri($"{_serviceSettings.RequestsUrl}/api/requests/{newRequestMessage.RequestId}");

                entity = await client.GetStringAsync(resourceUri);
            }

            var request = JsonConvert.DeserializeObject<Request>(entity);

            var calculationResult = _calculationService.Calculate(request.X);

            Console.WriteLine($"[{nameof(MessageService)}] Simulating long-running task / operation. Waiting {_simulationSettings.DelayInMilliseconds} ms");

            await Task.Delay(_simulationSettings.DelayInMilliseconds);

            await SendResult(request, calculationResult);
        }

        protected async Task SendResult(Request request, ICalculationResult calculationResult)
        {
            using (var client = new HttpClient())
            {
                var newResult = new NewResult
                {
                    Value  = calculationResult.Value,
                    Method = calculationResult.Method
                };

                var serializedNewResult = JsonConvert.SerializeObject(newResult);

                HttpResponseMessage responseMessage;

                using (var content = new StringContent(serializedNewResult)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                })
                {
                    responseMessage = await client.PostAsync($"{_serviceSettings.ResultsUrl}/api/requests/{request.RequestId}/result", content);
                }

                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successful");
                }
                else
                {
                    Console.WriteLine($"Unexpected response. Status Code {responseMessage.StatusCode}");
                    throw new InvalidOperationException();
                }
            }
        }
    }
}