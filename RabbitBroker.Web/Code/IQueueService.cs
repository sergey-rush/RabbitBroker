using System.Collections.Generic;

namespace RabbitBroker.Web.Code
{
    public interface IQueueService
    {
        void SendMessage(Message message);
        List<Message> GetMessages();
    }
}