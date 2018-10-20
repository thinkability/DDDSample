using Lamar;
using Messaging.Registry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Producer.Registry
{
    public class ProducerRegistry : ServiceRegistry
    {
        public ProducerRegistry()
        {
            ForConcreteType<ContinousTestDataProducer>();
            IncludeRegistry<MessagingRegistry>();
        }
    }
}