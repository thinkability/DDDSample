using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.Champagne.Commands;
using Domain.Aggregates.Champagne.Events;
using Domain.Aggregates.Champagne.ValueObjects;

namespace Domain.Aggregates.Champagne
{
    public class Champagne : Aggregate
    {
        public ChampagneName Name { get; private set; }
        public IEnumerable<GrapeBlend> Grapes { get; private set; }

        public void Execute(CreateChampagne cmd)
        {
            RaiseEvent(new ChampagneCreated(cmd.Id, cmd.Name));
        }

        public void Execute(RenameChampagne cmd)
        {
            RaiseEvent(new ChampagneRenamed(Id, Name, cmd.NewName));
        }

        public void Execute(UpdateGrapeBlend cmd)
        {
            if (cmd.Grapes.Sum(x => x.Percentage.Value) > 1)
                throw DomainError.Because("Grape blends cannot exceed 100% combined");

            if (cmd.Grapes.GroupBy(x => x.GrapeVariety).Any(x => x.Count() > 1))
                throw DomainError.Because("A grape can only appear once in the blend");
            
            RaiseEvent(new GrapeBlendUpdated(Id, cmd.Grapes));
        }
        
        protected override void RegisterHandlers()
        {
            Handle<ChampagneCreated>(e =>
            {
                Id = e.Id;
                Name = e.Name;
            });

            Handle<ChampagneRenamed>(e =>
            {
                Name = e.NewName;
            });

            Handle<GrapeBlendUpdated>(e =>
            {
                Grapes = e.UpdatedBlend;
            });
        }
    }
}