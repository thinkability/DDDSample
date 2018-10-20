using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Messaging.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Messaging.Kafka
{
    public class KafkaConsumer : IConsumer, IDisposable
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;
        private Consumer<Ignore, MessageEnvelope> _consumer;
        private TimeSpan _defaultTimeout = TimeSpan.FromSeconds(1);
        
        public KafkaConsumer(IEventHandlerFactory eventHandlerFactory, IOptions<MessagingConfig> options)
        {
            _eventHandlerFactory = eventHandlerFactory;
            
            var config = new Dictionary<string, object>()
            {
                ["bootstrap.servers"] = options.Value.KafkaBootstrapServers,
                ["group.id"] = "default-consumer-group",
                ["retries"] = 0,
                ["client.id"] = options.Value.KafkaClientId,
                ["batch.num.messages"] = 1,
                ["socket.blocking.max.ms"] = 1,
                ["socket.nagle.disable"] = true,
                ["queue.buffering.max.ms"] = 0,
                ["default.topic.config"] = new Dictionary<string, object>
                {
                    ["acks"] = 1
                }
            };
            
            _consumer = new Consumer<Ignore, MessageEnvelope>(config, new IgnoreDeserializer(), new JsonDeserializer<MessageEnvelope>());
        }

        public async Task Consume<TEvent>(string boundedContextName, string eventName)
        {
            var topic = $"{boundedContextName}.{eventName}";
            _consumer.Assign(new []{ new TopicPartitionOffset(topic, 0, Offset.Beginning) });
            _consumer.Subscribe(topic);
            
            while (true)
            {
                var exist = _consumer.Consume(out var msg, _defaultTimeout);

                if (!exist) continue;
                var eventType = Type.GetType(msg.Value.PayloadType);
                var @event = ((JObject)msg.Value.Payload).ToObject<TEvent>();

                var handler = _eventHandlerFactory.GetEventHandler<TEvent>();
                await handler.Handle(@event);
            }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }

    public interface IConsumer : IDisposable
    {
        Task Consume<TEvent>(string boundedContextName, string eventName);
    }
}