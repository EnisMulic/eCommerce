using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new();
            _eventTypes = new();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public event EventHandler<string> OnEventRemoved;

        public void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            AddHandler(typeof(TH), eventName, isDynamic: true);
        }

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            AddHandler(typeof(TH), eventName, isDynamic: false);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
        }

        private void AddHandler(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(i => i.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            if (isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }

        public void Clear()
        {
            _handlers.Clear();
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(i => i.Name == eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionForEvent(key);
        }

        public bool HasSubscriptionForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public void RemoveDynamicSubsciption<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            var handler = FindDynamicHandlerToRemove<TH>(eventName);
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var handler = FindHandlerToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            RemoveHandler(eventName, handler);
        }

        private void RemoveHandler(string eventName, SubscriptionInfo subscriptionInfo)
        {
            if (subscriptionInfo != null)
            {
                _handlers[eventName].Remove(subscriptionInfo);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(i => i.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }

                    RaiseOnEventRemoved(eventName);
                }
            }
        }

        private SubscriptionInfo FindDynamicHandlerToRemove<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            return FindHandlerToRemove(eventName, typeof(TH));
        }

        private SubscriptionInfo FindHandlerToRemove<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return FindHandlerToRemove(eventName, typeof(TH));
        }

        private SubscriptionInfo FindHandlerToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(i => i.HandlerType == handlerType);
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }
    }
}
