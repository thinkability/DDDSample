using System;
using System.Threading.Tasks;
using Domain.Aggregates.Champagne.Events;
using Messaging.Contracts;

namespace Projections.EventHandlers
{
    public class ChampagneProjectionHandler : IEventHandler<ChampagneCreated>, IEventHandler<ChampagneRenamed>
    {
        public Task Handle(ChampagneCreated @event)
        {
            Console.WriteLine("Received ChampagneCreated event:" + @event.Name.Value);
            return Task.CompletedTask;
        }

        public Task Handle(ChampagneRenamed @event)
        {
            Console.WriteLine($"Champagne renamed from '{@event.OldName.Value}' to '{@event.NewName.Value}'");
            return Task.CompletedTask;
        }
    }
}