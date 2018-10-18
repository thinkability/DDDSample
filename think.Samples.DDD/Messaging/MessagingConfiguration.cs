namespace Messaging
{
    public class MessagingConfiguration
    {
        public string BoundedContext { get; set; }
        public string Service { get; set; }
        public string KafkaBootstrapServers { get; set; }
        public string KafkaClientId { get; set; }
    }
}