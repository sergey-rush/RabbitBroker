using System.Collections.Generic;
using RabbitBroker.Web.Code;

namespace RabbitBroker.Web.Models
{
    public class DataModel
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public Message SelectedMessage { get; set; } = new Message();
    }
}