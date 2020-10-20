using System;
using System.Collections.Generic;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Events
{
    public class GrapeBlendUpdated : DomainEvent
    {
        public IEnumerable<GrapeBlend> UpdatedBlend { get; }

        public GrapeBlendUpdated(AggregateId id, IEnumerable<GrapeBlend> updatedBlend) : base(id)
        {
            UpdatedBlend = updatedBlend;
        }
    }
}