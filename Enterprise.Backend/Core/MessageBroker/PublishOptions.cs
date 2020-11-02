using System.Collections.Generic;

namespace Core.MessageBroker
{
    public class PublishOptions
    {
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public Dictionary<string, object> Headers { get; set; }
    }
}
