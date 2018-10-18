using System.Threading.Tasks;
using Domain.Persistence.Events;

namespace Messaging
   {
       public interface IEventPublisher
       {
           Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;
       }
   }