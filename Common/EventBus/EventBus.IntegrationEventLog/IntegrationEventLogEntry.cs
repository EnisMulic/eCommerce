using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;

namespace EventBus.IntegrationEventLog
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId)
        {
            EventId = @event.Id;
            EventTypeName = @event.GetType().FullName;
            State = EventState.NotPublished;
            TimesSent = 0;
            CreatedAt = @event.CreatedAt;
            Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            TransactionId = transactionId;
        }

        public Guid EventId { get; private set; }
        public string EventTypeName { get; private set; }
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split(".")?.Last();
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }
        public EventState State { get; set; }
        public int TimesSent { get; set; }
        public DateTime CreatedAt { get; private set; }
        public string Content { get; private set; }
        public Guid TransactionId { get; private set; }

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {
            IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) as IntegrationEvent;
            return this;
        }
    }
}
