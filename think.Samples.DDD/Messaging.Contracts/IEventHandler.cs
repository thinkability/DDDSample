using System.Threading.Tasks;

namespace Messaging.Contracts
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event);
    }
}