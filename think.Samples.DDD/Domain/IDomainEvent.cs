using System;

namespace Domain
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        EventMetadata Metadata { get; }
    }
}