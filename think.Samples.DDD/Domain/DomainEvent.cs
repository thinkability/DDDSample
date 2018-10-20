using System;

namespace Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(Guid id, EventMetadata metadata = null)
        {
            Id = id;
            Metadata = metadata;
        }

        public Guid Id { get; }
        public EventMetadata Metadata { get; set; }
    }
}