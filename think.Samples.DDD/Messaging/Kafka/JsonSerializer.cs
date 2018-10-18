using System.Collections.Generic;
using System.Text;
using Confluent.Kafka.Serialization;
using Domain.Persistence.Events;
using Newtonsoft.Json;

namespace Messaging.Kafka
{
    public class JsonSerializer : ISerializer<object>
    {
        public void Dispose()
        {
        }

        public byte[] Serialize(string topic, object data)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
        }

        public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
        {
            return config;
        }
    }
}