using Domain.Aggregates.Champagne.Commands;
using Domain.Aggregates.Champagne.Events;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne
{
    public class Champagne : Aggregate
    {
        public ChampagneName Name { get; private set; }
        
        
        
        public void Execute(CreateChampagne cmd)
        {
            RaiseEvent(new ChampagneCreated(cmd.Id.Value, cmd.Name));
        }

        public void Execute(RenameChampagne cmd)
        {
            RaiseEvent(new ChampagneRenamed(Id.Value, Name, cmd.NewName));
        }
        
        protected override void RegisterHandlers()
        {
            Handle<ChampagneCreated>(e =>
            {
                Id = new AggregateId(e.Id);
                Name = e.Name;
            });

            Handle<ChampagneRenamed>(e => { Name = e.NewName; });
        }
    }
}