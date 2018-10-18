using Lamar;
using Messaging.Registry;

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