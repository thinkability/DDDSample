using System.Threading.Tasks;
using Domain;

namespace Messaging.Contracts
   {
       public interface IEventPublisher
       {
           Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent;
       }
   }