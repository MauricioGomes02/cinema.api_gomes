using Domain.Ports;
using Infrastructure.Configurations.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infrastructure.Adapters
{
    public class RabbitMqAdapter : IEventBusPort
    {
        private readonly IOptions<RabbitMqAdapterConfigurationModel> _options;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IChannel _channel;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMqAdapter(IOptions<RabbitMqAdapterConfigurationModel> options)
        {
            _options = options;
            _connectionFactory = new ConnectionFactory()
            {
                HostName = options.Value.Host,
                Port = options.Value.Port,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : class
        {
            using var channel = await CreateChannel().ConfigureAwait(false);
            var eventName = @event.GetType().Name;
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var properties = new BasicProperties
            {
                Persistent = true
            };

            foreach (var relationship in _options.Value.Relationships)
            {
                await channel.ExchangeDeclareAsync(
                    relationship.Exchange,
                    ExchangeType.Direct,
                    durable: true,
                    autoDelete: false);

                foreach (var queue in relationship.Queues)
                {
                    await channel.QueueDeclareAsync(
                        queue: queue.Name,
                        durable: true,
                        exclusive: false,
                        autoDelete: false);

                    await channel.QueueBindAsync(queue.Name, relationship.Exchange, queue.RoutingKey);
                }
            }

            var exchange = _options.Value.Relationships.First(x => x.Queues.Any(y => y.RoutingKey == eventName)).Exchange;

            await channel
                .BasicPublishAsync(exchange: exchange, routingKey: eventName, mandatory: true, basicProperties: properties, body: body)
                .ConfigureAwait(false);
        }

        public async Task SubscribeAsync<TEvent>(Func<TEvent, Task> func)
            where TEvent : class
        {
            using var channel = await CreateChannel().ConfigureAwait(false);

            foreach (var relationship in _options.Value.Relationships)
            {
                await channel.ExchangeDeclareAsync(
                    relationship.Exchange,
                    ExchangeType.Direct,
                    durable: true,
                    autoDelete: false);

                foreach (var queue in relationship.Queues)
                {
                    await channel.QueueDeclareAsync(
                        queue: queue.Name,
                        durable: true,
                        exclusive: false,
                        autoDelete: false);

                    await channel.QueueBindAsync(queue.Name, relationship.Exchange, queue.RoutingKey);
                }
            }

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                var routingKey = args.RoutingKey;
                var body = Encoding.UTF8.GetString(args.Body.ToArray());
                var @event = JsonConvert.DeserializeObject<TEvent>(body);

                await func(@event).ConfigureAwait(false);
            };

            var eventName = typeof(TEvent).Name;
            var queueName = _options.Value.Relationships.First(x => x.Queues.Any(y => y.RoutingKey == eventName)).Queues.First(x => x.RoutingKey == eventName).Name;

            await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer).ConfigureAwait(false);
        }

        public Task UnSubscribeAsync<TEvent, TEventHandler>()
            where TEvent : class
            where TEventHandler : IEventHandler
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;

            try
            {
                _connection?.Dispose();
            }
            catch
            {
            }
        }

        private bool IsConnected => _connection is not null && _connection.IsOpen && !_disposed;

        private async Task<bool> TryConnect()
        {
            _connection = await _connectionFactory
                .CreateConnectionAsync()
                .ConfigureAwait(false);

            return IsConnected;
        }

        private async Task<IChannel> CreateChannel()
        {
            if (!IsConnected)
            {
                await TryConnect().ConfigureAwait(false);
            }

            return await _connection.CreateChannelAsync().ConfigureAwait(false);
        }
    }
}
