using System.Threading.Tasks;

namespace Messaging.Contracts
{
    public interface IEventHandler<TEvent>
    {
        Task Handle(TEvent @event);
    }
}