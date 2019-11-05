using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitBroker.Web.Code
{
    public class QueueService : IQueueService
    {
        static List<Message> messages = new List<Message>();
        ConnectionFactory factory = null;
        IConnection connection = null;
        IModel channel = null;

        public QueueService()
        {
            factory = new ConnectionFactory() {HostName = "localhost"};
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: "info",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                Message message = body.FromByteArray<Message>();
                messages.Add(message);
            };

            channel.BasicConsume(queue: "info",
                autoAck: true,
                consumer: consumer);
        }

        public void SendMessage(Message message)
        {
            var body = message.ToByteArray();

            channel.BasicPublish(exchange: "",
                routingKey: "info",
                basicProperties: null,
                body: body);
        }

        public List<Message> GetMessages()
        {
            return messages;
        }
    }
}