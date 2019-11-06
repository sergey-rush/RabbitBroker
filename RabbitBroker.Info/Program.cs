using RabbitBroker.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitBroker.Info
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = LogType.Info.ToString();
            var factory = new ConnectionFactory() {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    Message message = body.FromByteArray<Message>();
                    System.Console.WriteLine("UniqueId: {0} Info: {1} Created: {2:F}", message.UniqueId, message.Info,
                        message.Created);
                };
                channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);

                System.Console.WriteLine(" Press [enter] to exit.");
                System.Console.ReadLine();
            }
        }
    }
}
