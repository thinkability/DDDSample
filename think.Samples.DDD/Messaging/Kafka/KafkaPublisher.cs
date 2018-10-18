using System.Threading.Tasks;
using Confluent.Kafka;
using Domain.Persistence.Events;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka
{
    public class KafkaPublisher : IEventPublisher
    {
        private readonly ISerializingProducer<Null, object> _producer;
        private readonly MessagingConfiguration _config;

        public KafkaPublisher(ISerializingProducer<Null, object> producer, IOptions<MessagingConfiguration> config)
        {
            _producer = producer;
            _config = config.Value;
        }
        
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            var res = await _producer.ProduceAsync(GetTopicByConvention(@event), null, @event);

            if (res.Error.HasError)
                throw new PublishingException(res.Error.Reason);
        }

        private string GetTopicByConvention(object @event)
        {
            return $"{_config.BoundedContext}.{_config.Service}.{@event.GetType().Name}";
        }
    }
}