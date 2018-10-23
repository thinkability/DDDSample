using System;

namespace Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}