using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Persistence.Events;
using Marten;

namespace Domain.Persistence
{
    public interface IPublishingAggregateRepository
    {
        /// <summary>
        /// Stores the uncomitted events in the eventstore.
        /// Events are published via an <see cref="IEventPublisher"/>
        /// </summary>
        /// <param name="aggregate">The DDD aggregate to store events for</param>
        /// <param name="transientSession">If supplied, this session represents the transaction boundary. Remember to call SaveChanges yorself if you use this</param>
        Task StoreAsync(Aggregate aggregate, IDocumentSession transientSession = null);

        Task<T> LoadAsync<T>(Guid id, int? version = null) where T : Aggregate;

        IDocumentSession CreateSession();

        Task PublishEventsAsync(IEnumerable<IDomainEvent> events);
        IEnumerable<T> QueryRawEvents<T>(Func<T, bool> predicate);
    }
}