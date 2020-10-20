using System;

namespace Domain
{
    public interface IDomainEvent
    {
        AggregateId Id { get; }
    }
}