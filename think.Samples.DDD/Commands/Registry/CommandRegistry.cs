using Commands.Handlers.Champagne;
using Domain.Persistence.Registry;
using Lamar;
using Messaging.Contracts;

namespace Commands.Registry
{
    public class CommandRegistry : ServiceRegistry
    {
        public CommandRegistry()
        {
            For<ICommandHandler<CreateChampagneCommand, IdResponse>>().Use<ChampagneHandler>();
            For<ICommandHandler<RenameChampagneCommand>>().Use<ChampagneHandler>();
            
            IncludeRegistry<PersistenceRegistry>();
        }
    }
}