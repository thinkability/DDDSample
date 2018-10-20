using Lamar;
using Marten.Events;
using Messaging.Contracts;

namespace Messaging
{
    public class LamarEventHandlerFactory : IEventHandlerFactory
    {
        private readonly IServiceContext _context;

        public LamarEventHandlerFactory(IServiceContext context)
        {
            _context = context;
        }
        
        public IEventHandler<TEvent> GetEventHandler<TEvent>()
        {
            return _context.GetInstance<IEventHandler<TEvent>>();
        }
    }
}