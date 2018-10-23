using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Domain;
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

        private HashSet<Subscription> _subscriptions = new HashSet<Subscription>();

        private bool _isConsuming;

        public KafkaConsumer(IEventHandlerFactory eventHandlerFactory, IOptions<MessagingConfig> options)
        {
            _eventHandlerFactory = eventHandlerFactory;

            var config = new Dictionary<string, object>()
            {
                ["bootstrap.servers"] = options.Value.KafkaBootstrapServers,
                ["group.id"] = "default-consumer-group",
                ["retries"] = 0,
                ["client.id"] = options.Value.KafkaClientId ?? options.Value.Service,
                ["batch.num.messages"] = 1,
                ["socket.blocking.max.ms"] = 1,
                ["socket.nagle.disable"] = true,
                ["queue.buffering.max.ms"] = 0,
                ["default.topic.config"] = new Dictionary<string, object>
                {
                    ["acks"] = 1
                }
            };

            _consumer = new Consumer<Ignore, MessageEnvelope>(config, new IgnoreDeserializer(),
                new JsonDeserializer<MessageEnvelope>());
        }
        
        public async Task StartConsumer<TEvent>(Subscription<TEvent> subscription)
        {
            await StartConsumer(new[] {subscription});
        }

        public async Task StartConsumer(IEnumerable<Subscription> subscriptions)
        {
            _subscriptions = new HashSet<Subscription>(subscriptions);
            var topics = _subscriptions.Select(x => Convention.TopicName(x.BoundedContext, x.EventName));
            _consumer.Assign(topics.Select(x => new TopicPartition(x, 0)));
            _consumer.Subscribe(topics);

            _isConsuming = true;

            while (_isConsuming)
            {
                var exist = _consumer.Consume(out var msg, _defaultTimeout);

                if (!exist) continue;

                await HandleMessage(msg);
            }
        }
       
        public void StopConsumer()
        {
            _isConsuming = false;
        }

        private async Task HandleMessage(Message<Ignore, MessageEnvelope> msg)
        {
            var subscription = _subscriptions.SingleOrDefault(x =>
                string.Equals(Convention.TopicName(x.BoundedContext, x.EventName), msg.Topic));

            if (subscription == null)
                throw new ConsumingException("Unexpected message consumed: No subscription registered");

            var @event = ((JObject) msg.Value.Payload).ToObject(subscription.EventType);

            var factory = typeof(IEventHandlerFactory)
                .GetMethod(nameof(IEventHandlerFactory.GetEventHandler))
                .MakeGenericMethod(subscription.EventType);
            var handler = factory.Invoke(_eventHandlerFactory, null);

            var handle = typeof(IEventHandler<>)
                .MakeGenericType(subscription.EventType)
                .GetMethod("Handle");
            
            await (Task) handle.Invoke(handler, new[] {@event});
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }

    public interface IConsumer : IDisposable
    {
        Task StartConsumer<TEvent>(Subscription<TEvent> subscription);
        Task StartConsumer(IEnumerable<Subscription> subscriptions);
        void StopConsumer();
    }

    public abstract class Subscription
    {
        public string BoundedContext { get; protected set; }
        public string EventName { get; protected set; }
        public Type EventType { get; protected set; }

        public Subscription()
        {
        }
    }

    public class Subscription<TEvent> : Subscription
    {
        public Subscription(string boundedContext, string eventName)
        {
            BoundedContext = boundedContext;
            EventName = eventName;
            EventType = typeof(TEvent);
        }
    }
}