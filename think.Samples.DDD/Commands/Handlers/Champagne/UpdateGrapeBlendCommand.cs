using System;
using System.Collections.Generic;
using Messaging.Contracts;

namespace Commands.Handlers.Champagne
{
    public class UpdateGrapeBlendCommand : ICommand<Response>
    {
        public Guid ChampagneId { get; private set; }
        public IEnumerable<(double Percentage, string GrapeVariety)> Grapes { get; private set; }

        public UpdateGrapeBlendCommand(Guid champagneId, IEnumerable<(double Percentage, string GrapeVariety)> grapes)
        {
            ChampagneId = champagneId;
            Grapes = grapes;
        }
    }
}