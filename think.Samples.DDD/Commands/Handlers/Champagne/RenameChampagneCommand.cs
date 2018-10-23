using System;
using Messaging.Contracts;

namespace Commands.Handlers.Champagne
{
    public class RenameChampagneCommand : ICommand<Response>
    {
        public RenameChampagneCommand(Guid champagneId, string newName)
        {
            ChampagneId = champagneId;
            NewName = newName;
        }

        public Guid ChampagneId { get; private set; }
        public string NewName { get; private set; }
    }
}