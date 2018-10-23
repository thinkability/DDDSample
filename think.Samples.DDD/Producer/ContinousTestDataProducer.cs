using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
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
            var fixture = new Fixture();
            
            while (true)
            {
                var name = fixture.Create<string>();
                var id = await _commandRouter.RouteAsync<CreateChampagneCommand, IdResponse>(new CreateChampagneCommand(name));
                
                Console.WriteLine($"Champagne created: {id.Id} - {name}");
                await Task.Delay(2000);
            }
        }
    }
}