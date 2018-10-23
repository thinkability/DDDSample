namespace Messaging.Kafka
{
    public class Convention
    {
        public static string TopicName(string boundedContextName, string eventName)
        {
            return $"{boundedContextName}.{eventName}";
        }
    }
}