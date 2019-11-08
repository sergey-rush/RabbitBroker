using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitBroker.Core;
using RabbitBroker.Data;
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
            channel.QueueDeclare(queue: "Pay",
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
                RequestProvider requestProvider = new RequestProvider();
                //Task.Run(()=> requestProvider.ProcessFee(message.UserId, message.Amount));
                Thread t = new Thread(new ParameterizedThreadStart(requestProvider.ProcessFee));
                t.Start(message);
                Debug.WriteLine("Message: {0} received", message.Id);
            };

            channel.BasicConsume(queue: "Pay",
                autoAck: true,
                consumer: consumer);
        }

        public void SendMessage(Message message)
        {
            var body = message.ToByteArray();

            channel.BasicPublish(exchange: "",
                routingKey: "Pay",
                basicProperties: null,
                body: body);
        }

        public IEnumerable<Message> GetMessages(int length)
        {
            return messages.OrderByDescending(x=>x.Created).Take(length);
        }
    }
}