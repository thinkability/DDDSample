using System;
using System.Threading.Tasks;
using Domain.Aggregates.Champagne.Events;
using Messaging.Contracts;

namespace Projections.EventHandlers
{
    public class ChampagneProjectionHandler : IEventHandler<ChampagneCreated>
    {
        public Task Handle(ChampagneCreated @event)
        {
            Console.WriteLine("Champagne created: " + @event.Name.Value);
            return Task.CompletedTask;
        }
    }
}