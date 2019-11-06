using System;
using System.Collections.Generic;
using RabbitBroker.Core;
using RabbitMQ.Client;

namespace RabbitBroker.Emitter
{
    public class Producer
    {
        Random random = new Random();
        ConnectionFactory factory = null;
        IConnection connection = null;
        IModel channel = null;

        Dictionary<LogType, string> events = new Dictionary<LogType, string>();

        public Producer()
        {
            Init();
        }

        private void Init()
        {
            events.Add(LogType.Debug, "Debug: fine-grained statements concerning program state, typically used for debugging;");
            events.Add(LogType.Info, "Info: informational statements concerning program state, representing program events or behavior tracking;");
            events.Add(LogType.Warn, "Warn: statements that describe potentially harmful events or states in the program;");
            events.Add(LogType.Error, "Error: statements that describe non-fatal errors in the application; this level is used quite often for logging handled exceptions;");
            events.Add(LogType.Fatal, "Fatal: statements representing the most severe of error conditions, assumedly resulting in program termination.");

            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            QueueDeclareOk result = channel.QueueDeclare(queue: "info",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void Emit()
        {
            int index = random.Next(0, events.Count);
            LogType logType = (LogType) index;
            var entry = events[logType];
            Message message = new Message();
            message.Info = entry;

            var body = message.ToByteArray();

            channel.BasicPublish(exchange: "",
                routingKey: logType.ToString(),
                basicProperties: null,
                body: body);

            Console.WriteLine("Emitted {0}", logType.ToString());
        }
    }
}