using System.Threading.Tasks;

namespace Messaging.Contracts
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand<Response>
    {
        Task<Response> Handle(TCommand cmd);
    }

    public interface ICommandHandler<in TCommand, TResponse> 
        where TCommand : ICommand<TResponse>
    {
        Task<TResponse> Handle(TCommand cmd);
    }
}