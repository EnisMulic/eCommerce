using System;
using System.Collections.Generic;

namespace EventBus
{
    public class EventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        public bool IsEmpty => throw new NotImplementedException();

        public event EventHandler<string> OnEventRemoved;

        public void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public string GetEventKey<T>()
        {
            throw new NotImplementedException();
        }

        public Type GetEventTypeByName(string eventName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionForEvent<T>() where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }

        public bool HasSubscriptionForEvent(string eventName)
        {
            throw new NotImplementedException();
        }

        public void RemoveDynamicSubsciption<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            throw new NotImplementedException();
        }

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
