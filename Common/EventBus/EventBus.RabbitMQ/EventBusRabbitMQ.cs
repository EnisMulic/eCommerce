using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text.Json;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        private const string BROKER_NAME = "eCommerce_event_bus";

        private readonly IPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;

        public EventBusRabbitMQ(IPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }    
            
            var eventName = @event.GetType().Name;

            using var channel = _persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: BROKER_NAME, type: "direct");

            var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });

            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            _logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            channel.BasicPublish(
                exchange: BROKER_NAME,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
