using System;

namespace RabbitBroker.Core
{
    [Serializable]
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get;set; }
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        public string Info { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}