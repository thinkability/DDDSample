using Lamar;
using Messaging.Registry;
using Projections.Registry;

namespace ProjectionsHost.Registry
{
    public class ProjectionsHostRegistry : ServiceRegistry
    {
        public ProjectionsHostRegistry()
        {
            IncludeRegistry<ProjectionsRegistry>();
            IncludeRegistry<MessagingRegistry>();
        }
    }
}