using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne.Commands
{
    public class RenameChampagne
    {
        public RenameChampagne(AggregateId id, ChampagneName newName)
        {
            Id = id;
            NewName = newName;
        }

        public AggregateId Id { get; private set; }
        public ChampagneName NewName { get; private set; }
    }
}