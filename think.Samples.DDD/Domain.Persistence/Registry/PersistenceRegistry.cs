using Lamar;
using Marten;
using Messaging.Registry;
using Microsoft.Extensions.Options;

namespace Domain.Persistence.Registry
{
    public class PersistenceRegistry : ServiceRegistry
    {
        public PersistenceRegistry()
        {
            For<IDocumentStore>().Use(ctx => ConfigureEventstore(ctx));
            
            IncludeRegistry<MessagingRegistry>();
            
            Scan(x =>
            {
                x.AssemblyContainingType<PersistenceRegistry>();
                x.WithDefaultConventions();
            });
        }
                                                                
        private IDocumentStore ConfigureEventstore(IServiceContext ctx)
        {
            return DocumentStore.For(opt =>
            {
                var config = ctx.GetInstance<IOptions<PersistenceConfiguration>>().Value;
                
                opt.Connection(config.ConnectionString);

                opt.CreateDatabases = db => db.ForTenant(config.Tenant);
                opt.AutoCreateSchemaObjects = AutoCreate.All;
                opt.DatabaseSchemaName = config.Schema;
            });
        }
    }
}