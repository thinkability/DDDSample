using System;

namespace Messaging.Kafka
{
    public class Subscription<TEvent> : Subscription
    {
        public Subscription(string boundedContext, string eventName)
        {
            BoundedContext = boundedContext;
            EventName = eventName;
            EventType = typeof(TEvent);
        }
    }

    public abstract class Subscription
    {
        public string BoundedContext { get; protected set; }
        public string EventName { get; protected set; }
        public Type EventType { get; protected set; }

        public Subscription()
        {
        }
    }
}