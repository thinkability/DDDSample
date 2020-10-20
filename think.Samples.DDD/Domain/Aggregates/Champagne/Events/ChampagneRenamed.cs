using System;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Events
{
    public class ChampagneRenamed : DomainEvent
    {
        public ChampagneName OldName { get; private set; }
        public ChampagneName NewName { get; private set; }
        
        public ChampagneRenamed(AggregateId id, ChampagneName oldName, ChampagneName newName) : base(id)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}