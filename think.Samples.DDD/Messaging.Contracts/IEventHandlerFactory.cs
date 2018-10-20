using System;

namespace Messaging.Contracts
{
    public interface IEventHandlerFactory
    {
        IEventHandler<TEvent> GetEventHandler<TEvent>();
    }
}