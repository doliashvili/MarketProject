using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ.Connection
{
    public class RabbitConnectionWrapper : IRabbitConnectionWrapper
    {
        public IConnection Connection { get; }
        public RabbitMqConfig Configuration { get; }


        public RabbitConnectionWrapper(RabbitMqConfig config)
        {
            Configuration = config;
            var connectionFactory = new ConnectionFactory()
            {
                UserName = config.UserName,
                Password = config.Password,
                HostName = config.Host,
                Port = config.Port,
                VirtualHost = config.VirtualHost,
            };

            Connection = connectionFactory.CreateConnection();
        }
    }
}
