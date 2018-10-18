using Domain.Persistence;

namespace Commands.Handlers
{
    public abstract class CommandHandlerBase
    {
        protected readonly IPublishingAggregateRepository AggregateRepo;

        protected CommandHandlerBase(IPublishingAggregateRepository aggregateRepo)
        {
            AggregateRepo = aggregateRepo;
        }
    }
}