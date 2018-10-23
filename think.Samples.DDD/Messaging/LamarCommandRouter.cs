using System.Threading;
using System.Threading.Tasks;
using Lamar;
using Messaging.Contracts;

namespace Messaging
{
    public class LamarCommandRouter : ICommandRouter
    {
        private readonly IContainer _container;

        public LamarCommandRouter(IContainer container)
        {
            _container = container;
        }
        
        public async Task<TResponse> RouteAsync<TCommand, TResponse>(TCommand command,
            CancellationToken cancellationToken = default(CancellationToken)) 
            where TCommand : ICommand<TResponse>
        {
            return await _container.GetInstance<ICommandHandler<TCommand, TResponse>>().Handle(command);
        }

        public async Task RouteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand<Response>
        {
            await _container.GetInstance<ICommandHandler<TCommand>>().Handle(command);
        }
    }
}