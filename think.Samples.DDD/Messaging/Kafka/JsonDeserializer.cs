using System.Collections.Generic;
using System.Text;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;

namespace Messaging.Kafka
{
    public class JsonDeserializer : IDeserializer<object>
    {
        public void Dispose()
        {
        }

        public object Deserialize(string topic, byte[] data)
        {
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data));
        }

        public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
        {
            return config;
        }
    }
}