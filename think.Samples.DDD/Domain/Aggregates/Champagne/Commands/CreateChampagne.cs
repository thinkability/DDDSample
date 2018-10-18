using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Commands
{
    public class CreateChampagne
    {
        public AggregateId Id { get; private set; }
        public ChampagneName Name { get; private set; }

        public CreateChampagne(AggregateId id, ChampagneName name)
        {
            Id = id;
            Name = name;
        }
    }
}