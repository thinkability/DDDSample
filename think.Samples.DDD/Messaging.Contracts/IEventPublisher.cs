using System.Threading.Tasks;
using Domain;

namespace Messaging.Contracts
   {
       public interface IEventPublisher
       {
           Task PublishAsync<TEvent>(TEvent @event, EventMetadata metadata) where TEvent : IDomainEvent;
       }
   }