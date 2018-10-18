using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Commands.Handlers.Champagne;
using Messaging.Contracts;

namespace Producer
{
    public class ContinousTestDataProducer
    {
        private readonly ICommandRouter _commandRouter;

        public ContinousTestDataProducer(ICommandRouter commandRouter)
        {
            _commandRouter = commandRouter;
        }

        public async Task Produce()
        {
            while (!Console.KeyAvailable)
            {
                var id = await _commandRouter.RouteAsync<CreateChampagneCommand, IdResponse>(new CreateChampagneCommand("My new pagne"));
                
                Console.WriteLine($"Champagne created: {id}");
                Thread.Sleep(2000);
            }
        }
    }
}