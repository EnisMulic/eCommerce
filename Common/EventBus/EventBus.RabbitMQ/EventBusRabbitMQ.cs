using EventBus.Extensions;
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
        private readonly IEventBusSubscriptionsManager _subsManager;

        private readonly string _queueName;

        public EventBusRabbitMQ(
            IPersistentConnection persistentConnection,
            ILogger<EventBusRabbitMQ> logger,
            IEventBusSubscriptionsManager subsManager, 
            string queueName)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _subsManager = subsManager;
            _queueName = queueName;
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
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation(
                "Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            Subscribe(eventName);
            _subsManager.AddSubscription<T, TH>();
        }

        public void SubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation(
                "Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            Subscribe(eventName);
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        private void Subscribe(string eventName)
        {
            var containsKey = _subsManager.HasSubscriptionForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                using var channel = _persistentConnection.CreateModel();
                channel.QueueBind(queue: _queueName, exchange: BROKER_NAME, routingKey: eventName);
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

            _subsManager.RemoveDynamicSubsciption<TH>(eventName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
