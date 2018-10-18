using System;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Events
{
    public class ChampagneCreated : DomainEvent
    {
        public ChampagneName Name { get; private set; }
        
        public ChampagneCreated(AggregateId id, ChampagneName name, EventMetadata metadata = null) : base(id, metadata)
        {
            Name = name;
        }
    }
}