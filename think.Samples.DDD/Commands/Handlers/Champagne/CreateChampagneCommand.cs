using Messaging.Contracts;

namespace Commands.Handlers.Champagne
{
    public class CreateChampagneCommand : ICommand<IdResponse>
    {
        public CreateChampagneCommand(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}