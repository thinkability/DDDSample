using System.Collections.Generic;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Commands
{
    public class UpdateGrapeBlend
    {
        public AggregateId Id { get; private set; }
        public IEnumerable<GrapeBlend> Grapes { get; private set; }

        public UpdateGrapeBlend(AggregateId id, IEnumerable<GrapeBlend> grapes)
        {
            Id = id;
            Grapes = grapes;
        }
    }
}