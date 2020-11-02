using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ.Connection
{
    public interface IRabbitConnectionWrapper
    {
        IConnection Connection { get; }
        RabbitMqConfig Configuration { get; }
    }
}
