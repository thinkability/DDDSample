using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.Champagne.Commands;
using Domain.Aggregates.Champagne.ValueObjects;
using Domain.Persistence;
using Messaging.Contracts;

namespace Commands.Handlers.Champagne
{
    public class ChampagneHandler : CommandHandlerBase, 
        ICommandHandler<CreateChampagneCommand, IdResponse>, 
        ICommandHandler<RenameChampagneCommand>,
        ICommandHandler<UpdateGrapeBlendCommand>
    {
        public ChampagneHandler(IPublishingAggregateRepository aggregateRepo) : base(aggregateRepo)
        {
        }

        public async Task<IdResponse> Handle(CreateChampagneCommand cmd)
        {
            var id = Guid.NewGuid();
            
            var pagne = new Domain.Aggregates.Champagne.Champagne();
            pagne.Execute(new Domain.Aggregates.Champagne.Commands.CreateChampagne(new AggregateId(id), new ChampagneName(cmd.Name)));

            await AggregateRepo.StoreAsync(pagne);
            
            return new IdResponse(id);
        }

        public async Task<Response> Handle(RenameChampagneCommand cmd)
        {
            var pagne =
                await AggregateRepo.LoadAsync<Domain.Aggregates.Champagne.Champagne>(new AggregateId(cmd.ChampagneId));

            if (pagne == null)
                throw DomainError.Because($"Champagne with Id '{cmd.ChampagneId}' could not be found");
            
            pagne.Execute(new RenameChampagne(new AggregateId(cmd.ChampagneId), new ChampagneName(cmd.NewName)));

            await AggregateRepo.StoreAsync(pagne);
            
            return Response.Success();
        }

        public async Task<Response> Handle(UpdateGrapeBlendCommand cmd)
        {
            var pagne =
                await AggregateRepo.LoadAsync<Domain.Aggregates.Champagne.Champagne>(new AggregateId(cmd.ChampagneId));

            if (pagne == null)
                throw DomainError.Because($"Champagne with Id '{cmd.ChampagneId}' could not be found");

            var grapes = cmd.Grapes.Select(x =>
                new GrapeBlend(new GrapeBlendPercentage(x.Percentage), new GrapeVariety(x.GrapeVariety)));
            
            pagne.Execute(new UpdateGrapeBlend(new AggregateId(cmd.ChampagneId), grapes));

            await AggregateRepo.StoreAsync(pagne);
            
            return Response.Success();        
        }
    }
}