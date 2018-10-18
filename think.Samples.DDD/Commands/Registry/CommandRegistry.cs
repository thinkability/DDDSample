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
            
            Scan(x =>
            {
                x.AssemblyContainingType<CommandRegistry>();
                x.WithDefaultConventions();
            });
            
            IncludeRegistry<PersistenceRegistry>();
        }
    }
}