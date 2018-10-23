using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Kafka
{
    public interface IConsumer : IDisposable
    {
        Task StartConsumer<TEvent>(Subscription<TEvent> subscription);
        Task StartConsumer(IEnumerable<Subscription> subscriptions);
        void StopConsumer();
    }
}