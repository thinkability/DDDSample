using Domain;

namespace Messaging.Contracts
{
    public class MessageEnvelope
    {
        public object Payload { get; private set; }
        public string PayloadType { get; private set; }
        public EventMetadata Metadata { get; private set; }

        public MessageEnvelope(object payload, EventMetadata metadata)
        {
            Payload = payload;
            PayloadType = payload.GetType().Name;
            Metadata = metadata;
        }
    }
}