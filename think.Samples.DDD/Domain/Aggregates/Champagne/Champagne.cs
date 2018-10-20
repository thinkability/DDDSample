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
        
        protected override void RegisterHandlers()
        {
            Handle<ChampagneCreated>(e =>
            {
                Id = new AggregateId(e.Id);
                Name = e.Name;
            });
        }
    }
}