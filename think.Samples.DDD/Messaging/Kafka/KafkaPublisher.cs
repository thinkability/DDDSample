using System.Threading.Tasks;
using Confluent.Kafka;
using Domain;
using Messaging.Contracts;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka
{
    public class KafkaPublisher : IEventPublisher
    {
        private readonly ISerializingProducer<Null, object> _producer;
        private readonly MessagingConfig _config;

        public KafkaPublisher(ISerializingProducer<Null, object> producer, IOptions<MessagingConfig> config)
        {
            _producer = producer;
            _config = config.Value;
        }
        
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var envelope = new MessageEnvelope(@event, @event.Metadata);
            
            var res = await _producer.ProduceAsync(GetTopicByConvention<TEvent>(@event), null, envelope);

            if (res.Error.HasError)
                throw new PublishingException(res.Error.Reason);
        }

        private string GetTopicByConvention<TEvent>(TEvent @event)
        {
            return $"{_config.BoundedContext}.{@event.GetType().Name}";
        }
    }
}