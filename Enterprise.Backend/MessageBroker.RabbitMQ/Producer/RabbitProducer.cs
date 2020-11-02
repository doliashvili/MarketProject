using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.MessageBroker;
using MessageBroker.RabbitMQ.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace MessageBroker.RabbitMQ.Producer
{
    public class RabbitProducer : IMessageProducer
    {
        private readonly IModel _channel;
        private readonly RabbitMqConfig _configuration;
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1,1);
        
        public RabbitProducer(IRabbitConnectionWrapper rabbitConnectionWrapper)
        {
            _configuration = rabbitConnectionWrapper.Configuration;
            _channel = rabbitConnectionWrapper.Connection.CreateModel();
        }

        public async Task PublishAsync<T>(T message, PublishOptions publishOptions = default, 
            CancellationToken cancellationToken = default) where T : IMessage
        {
            await Semaphore.WaitAsync(cancellationToken);
            try
            {
                var options = SetupExchange(publishOptions);
                Send(message, options);
            }
            finally
            {
                Semaphore.Release();
            }
        }
        


        public async Task PublishAsync<T>(IEnumerable<T> messages, PublishOptions publishOptions = default, 
            CancellationToken cancellationToken = default) where T : IMessage
        {
            await Semaphore.WaitAsync(cancellationToken);
            try
            {
                var options = SetupExchange(publishOptions);
                messages.ToList().ForEach(message =>
                {
                    Send(message, options);
                });
            }
            finally
            {
                Semaphore.Release();
            }
        }


        private void Send<T>(T message, PublishOptions options) where T : IMessage
        {
            var routingKey = message.GetType().Name;
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(options.Exchange, routingKey, false, body: body);
        }


        private PublishOptions SetupExchange(PublishOptions publishOptions)
        {
            if (publishOptions == default)
            {
                publishOptions = new PublishOptions()
                {
                    ExchangeType = _configuration.DefaultExchangeType,
                    Exchange = _configuration.DefaultExchange
                };
            }
            else
            {
                publishOptions = new PublishOptions
                {
                    Exchange = publishOptions.Exchange ?? _configuration.DefaultExchange,
                    ExchangeType = publishOptions.ExchangeType ?? _configuration.DefaultExchangeType,
                    Headers = publishOptions.Headers
                };
            }

            _channel.ExchangeDeclare(
                publishOptions.Exchange,
                publishOptions.ExchangeType,
                true,
                false,
                publishOptions.Headers);

            return publishOptions;
        }
    }
}
