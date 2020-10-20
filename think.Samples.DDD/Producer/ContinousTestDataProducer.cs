using System;
using System.Collections.Generic;
using System.Linq;
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
            var random = new Random();
            
            var styles = new[] {"Brut", "Rosé", "On Ice", "Vintage", ""};
            var characters = new[] {"Grand Cru", "Premier Cru", "Prestige Cuvée", "Cuvée"};
            var dosages = new[] {"Extra-Brut", "Brut", "Sec", "Demi-Sec", "Doux"};
            var estates = new[] {"Lars", "DAD", "Tour-de-Code", "Vinofactorie"};
            var labels = new[] {"1973", "2003", "1998", "1996", "Black Label", "Nocturne"};

            T SelectOne<T>(IEnumerable<T> options)
            {
                var idx = random.Next(0, options.Count() - 1);
                return options.ElementAt(idx);
            }

            var champagnes = new List<Guid>();

            while (true)
            {
                var name =
                    $"{SelectOne(estates)}'s {SelectOne(characters)} {SelectOne(labels)} {SelectOne(styles)} {SelectOne(dosages)}";

                if (!champagnes.Any() || random.Next(0, 10) > 5)
                {
                    var response =
                        await _commandRouter.RouteAsync<CreateChampagneCommand, IdResponse>(
                            new CreateChampagneCommand(name));
                    champagnes.Add(response.Id);
                    Console.WriteLine($"Champagne created: {name} ({response.Id})");
                }
                else
                {
                    var id = SelectOne(champagnes);

                    await _commandRouter.RouteAsync<RenameChampagneCommand>(new RenameChampagneCommand(id, name));
                    Console.WriteLine($"Champagne renamed: {id} - New name: {name}");
                }

                await Task.Delay(random.Next(1000, 3000));
            }
        }
    }
}