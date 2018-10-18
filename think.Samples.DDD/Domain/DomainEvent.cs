namespace Domain
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(AggregateId id, EventMetadata metadata = null)
        {
            Id = id;
            Metadata = metadata;
        }

        public AggregateId Id { get; }
        public EventMetadata Metadata { get; set; }
    }
}