namespace MessageBroker.RabbitMQ
{
    public class RabbitMqConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string DefaultExchange { get; set; }
        public string DefaultExchangeType { get; set; }
    }
}
