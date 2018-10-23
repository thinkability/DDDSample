using System;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Events
{
    public class ChampagneCreated : DomainEvent
    {
        public ChampagneName Name { get; private set; }
        
        public ChampagneCreated(Guid id, ChampagneName name) : base(id)
        {
            Name = name;
        }
    }
}