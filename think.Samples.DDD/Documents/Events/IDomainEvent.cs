using System;

namespace Domain.Persistence.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        EventMetadata Metadata { get; }
    }
}