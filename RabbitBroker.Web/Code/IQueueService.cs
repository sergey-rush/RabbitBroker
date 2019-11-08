using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitBroker.Core;

namespace RabbitBroker.Web.Code
{
    public interface IQueueService
    {
        void SendMessage(Message message);
        IEnumerable<Message> GetMessages(int length);
    }
}