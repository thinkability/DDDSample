using System.Collections.Generic;
using System.Text;
using Commands.Registry;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Lamar;
using Messaging.Contracts;
using Messaging.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Messaging.Registry
{
    public class MessagingRegistry : ServiceRegistry
    {
        public MessagingRegistry()
        {   
            For<Producer>().Use(ctx =>
            {
                var options = ctx.GetInstance<IOptions<MessagingConfig>>().Value;
                
                var config = new Dictionary<string, object>()
                {
                    ["bootstrap.servers"] = options.KafkaBootstrapServers,
                    ["retries"] = 0,
                    ["client.id"] = options.KafkaClientId ?? options.Service,
                    ["batch.num.messages"] = 1,
                    ["socket.blocking.max.ms"] = 1,
                    ["socket.timeout.ms"] = 500,
                    ["socket.nagle.disable"] = true,
                    ["queue.buffering.max.ms"] = 0,
                    ["default.topic.config"] = new Dictionary<string, object>
                    {
                        ["acks"] = 1
                    }
                };
                
                return new Producer(config);
            }).Singleton();
            
            For<ISerializingProducer<Null, object>>().Use(ctx =>
                ctx.GetInstance<Producer>()
                    .GetSerializingProducer(new NullSerializer(), new JsonSerializer<object>()));

            For<IConsumer>().Use<KafkaConsumer>();
            
            For<ICommandRouter>().Use<LamarCommandRouter>();
            For<IEventPublisher>().Use<KafkaPublisher>();

            For<IEventHandlerFactory>().Use<LamarEventHandlerFactory>();
            
            Scan(x =>
            {
                x.AssemblyContainingType<MessagingRegistry>();
                x.WithDefaultConventions();
            });
            
            IncludeRegistry<CommandRegistry>();
        }
    }
}