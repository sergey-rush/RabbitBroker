using System.Collections.Generic;
using RabbitBroker.Core;

namespace RabbitBroker.Web.Code
{
    public interface IQueueService
    {
        void SendMessage(Message message);
        List<Message> GetMessages();
    }
}