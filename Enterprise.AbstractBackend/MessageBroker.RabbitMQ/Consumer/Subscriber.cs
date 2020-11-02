using System;
using System.Reflection;
using System.Text;
using System.Threading;
using Core.MessageBroker;
using MessageBroker.RabbitMQ.Connection;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker.RabbitMQ.Consumer
{
    public class Subscriber : ISubscriber
    {
        private readonly Type _consumerType;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitConnectionWrapper _connection;
        private IModel _channel;
        private EventingBasicConsumer _basicConsumer;
        private readonly string _exchange;
        private readonly string _routingKey;
        private readonly string _queueName;
        private readonly bool _needRequeue;
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1,1);

        //TODO logging, stoppingTokens and dynamic exchangeTypes

        public Subscriber(Type consumerType, IServiceProvider serviceProvider)
        {
            _consumerType = consumerType;
            _serviceProvider = serviceProvider;
            _connection = serviceProvider.GetRequiredService<IRabbitConnectionWrapper>();
            var attribute = consumerType.GetCustomAttribute<MessageHandlerAttribute>();

            if (null == attribute)
                throw new Exception("MessageHandler attribute not found, bad code implementation in assembly scanner library");

            _exchange = attribute.Exchange;
            _routingKey = attribute.RoutingKey;
            _needRequeue = attribute.RequeueOnFail;
            _queueName = $"{_exchange}.{_routingKey}->{consumerType.FullName}";
        }

        private async void ReceivedEventHandler(object sender, BasicDeliverEventArgs e)
        {
            await Semaphore.WaitAsync();

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService(_consumerType) as IMessageConsumer;
                var bodyString = Encoding.UTF8.GetString(e.Body.ToArray());
                // ReSharper disable once PossibleNullReferenceException
                var result = await handler.ConsumeAsync(bodyString);

                switch (result)
                {
                    case HandleResult.Success:
                        _channel.BasicAck(e.DeliveryTag, false);
                        break;
                    case HandleResult.Fail:
                        _channel.BasicNack(e.DeliveryTag, false, _needRequeue);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Semaphore.Release();
            }
        }


        public void StartConsuming()
        {
            _channel = _connection.Connection.CreateModel();
            _channel.ExchangeDeclare(_exchange, ExchangeType.Direct, true, false);
            _channel.QueueDeclare(_queueName, true, false, false);
            _channel.QueueBind(_queueName, _exchange, _routingKey);
            _basicConsumer = new EventingBasicConsumer(_channel);
            _basicConsumer.Received += ReceivedEventHandler;
            _channel.BasicConsume(_queueName, false, _basicConsumer);
        }
    }
}
