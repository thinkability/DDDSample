using System;

namespace Messaging.Kafka
{
    public class PublishingException : Exception
    {
        public PublishingException(string errorReason) : base(errorReason)
        {
        }
    }
}