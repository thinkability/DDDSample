namespace Domain
{
    public interface IDomainEvent
    {
        AggregateId Id { get; }
        EventMetadata Metadata { get; }
    }
}