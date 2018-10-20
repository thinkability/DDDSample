namespace Messaging
{
    public class MessagingConfig
    {
        public string BoundedContext { get; set; }
        public string Service { get; set; }
        public string KafkaBootstrapServers { get; set; }
        public string KafkaClientId { get; set; }
        
    }
}