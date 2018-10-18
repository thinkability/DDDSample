using System.Threading;
using System.Threading.Tasks;

namespace Messaging.Contracts
{
    public interface ICommandRouter
    {
        Task<TResponse> RouteAsync<TCommand, TResponse>(TCommand command,
            CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand<TResponse>;
        
        Task RouteAsync<TCommand>(TCommand command, CancellationToken cancellationToken) 
            where TCommand : ICommand<Response>;
    }
    
    public interface ICommand<out TResult>{}
}