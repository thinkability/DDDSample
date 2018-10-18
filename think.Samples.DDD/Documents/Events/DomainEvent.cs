using System;

namespace Domain.Persistence.Events
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(Guid id, EventMetadata metadata)
        {
            Id = id;
            Metadata = metadata;
        }

        public Guid Id { get; }
        public EventMetadata Metadata { get; }
    }
}