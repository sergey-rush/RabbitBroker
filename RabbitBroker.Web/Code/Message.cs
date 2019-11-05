using System;

namespace RabbitBroker.Web.Code
{
    [Serializable]
    public class Message
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public string Info { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}