using System;
using RabbitBroker.Core;

namespace RabbitBroker.Data
{
    public abstract class DataAccess
    {
        protected virtual string ConnectionString
        {
            get
            {
                return Settings.DefaultConnection;
            }
        }
    }
}
