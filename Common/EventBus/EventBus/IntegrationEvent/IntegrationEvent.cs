using System;

namespace EventBus
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}