using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Marten;
using Messaging.Contracts;

namespace Domain.Persistence
{
    public sealed class PublishingAggregateRepository : IPublishingAggregateRepository
    {
        private readonly IDocumentStore _store;
        private readonly IEventPublisher _eventPublisher;

        public PublishingAggregateRepository(IDocumentStore store, IEventPublisher eventPublisher)
        {
            _store = store;
            _eventPublisher = eventPublisher;
        }

        /// <inheritdoc />
        public async Task StoreAsync(Aggregate aggregate, IDocumentSession transientSession = null)
        {
            using (var session = transientSession ?? _store.OpenSession())
            {
                // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
                var events = aggregate.GetUncommittedEvents().ToArray();
                
                session.Events.Append(aggregate.Id.Value, events);
                
                //Only save and publish changes if it is our own session (meaning none supplied)
                if (transientSession == null)
                {
                    await session.SaveChangesAsync();
                    await PublishEventsAsync(events);
                    
                    // Once successfully persisted, clear events from list of uncommitted events
                    aggregate.ClearUncommittedEvents();   
                }
            }         
        }        

        private static readonly MethodInfo ApplyEvent = typeof(Aggregate).GetMethod("ApplyEvent", BindingFlags.Instance | BindingFlags.NonPublic);

        public async Task<T> LoadAsync<T>(AggregateId id, int? version = null) where T : Aggregate
        {
            IReadOnlyList<Marten.Events.IEvent> events;
            using (var session = _store.LightweightSession())
            {
                events = await session.Events.FetchStreamAsync(id.Value, version ?? 0);                
            }

            if (events == null || !events.Any()) throw new InvalidOperationException($"No aggregate by id {id}.");
            
            var instance = Activator.CreateInstance(typeof(T), true);                
            // Replay our aggregate state from the event stream
            events.Aggregate(instance, (o, @event) => ApplyEvent.Invoke(instance, new [] { @event.Data }));
            return (T)instance;

        }

        /// <summary>
        /// Queries the entire event stream for events of type T. Use this with caution and only for debug
        /// </summary>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> QueryRawEvents<T>(Func<T, bool> predicate)
        {
            using (var session = _store.LightweightSession())
            {
                return session.Events.QueryRawEventDataOnly<T>().Where(predicate);
            }
        }

        public IDocumentSession CreateSession()
        {
            return _store.OpenSession();
        }

        public async Task PublishEventsAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                //TODO: Get metadata from eventstream
                await _eventPublisher.PublishAsync(@event, new EventMetadata(DateTimeOffset.Now, Guid.Empty, 0));
            }
        }
    }
}