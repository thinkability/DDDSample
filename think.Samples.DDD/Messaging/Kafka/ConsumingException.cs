using System;

namespace Messaging.Kafka
{
    public class ConsumingException : Exception
    {
        public ConsumingException(string message) : base(message)
        {
            
        }
        
    }
}