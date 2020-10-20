using System;

namespace Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(AggregateId id)
        {
            Id = id;
        }

        public AggregateId Id { get; }
    }
}