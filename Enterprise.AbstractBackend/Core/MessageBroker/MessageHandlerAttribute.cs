using System;

namespace Core.MessageBroker
{
    /// <summary>
    /// Marks handlers as a message broker handler and saves routing information
    /// </summary>
    public class MessageHandlerAttribute : Attribute
    {
        /// <summary>
        /// Exchange, default one exchange per micro service
        /// </summary>
        public string Exchange { get; }

        /// <summary>
        /// Routing key, it match name of event
        /// </summary>
        public string RoutingKey { get; }

        /// <summary>
        /// Requeue failed messages
        /// </summary>
        public bool RequeueOnFail { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="routingKey"></param>
        /// <param name="requeueOnFail"></param>
        public MessageHandlerAttribute(string exchange, string routingKey, bool requeueOnFail = false)
        {
            Exchange = exchange;
            RoutingKey = routingKey;
            RequeueOnFail = requeueOnFail;
        }
    }
}
