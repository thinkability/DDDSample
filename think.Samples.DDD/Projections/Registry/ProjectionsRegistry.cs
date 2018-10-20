using Lamar;
using Messaging.Contracts;
using Projections.EventHandlers;

namespace Projections.Registry
{
    public class ProjectionsRegistry : ServiceRegistry
    {
        public ProjectionsRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<ProjectionsRegistry>();
                x.WithDefaultConventions();
                x.AddAllTypesOf(typeof(IEventHandler<>));
            });
        }
    }
}