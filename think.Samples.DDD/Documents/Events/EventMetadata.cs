using System;

namespace Domain.Persistence.Events
{
    public class EventMetadata
    {
        public EventMetadata(DateTimeOffset eventDate, Guid correlationId, int sequence)
        {
            EventDate = eventDate;
            CorrelationId = correlationId;
            Sequence = sequence;
        }

        public DateTimeOffset EventDate { get; }
        public Guid CorrelationId { get; }
        public int Sequence { get; }
    }
}