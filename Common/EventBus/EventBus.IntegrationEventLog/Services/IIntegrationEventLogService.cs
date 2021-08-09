using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBus.IntegrationEventLog.Services
{
    public interface IIntegrationEventLogService
    {
        Task<IEnumerable<IntegrationEventLogEntry>> GetEventsPendingToPublishAsync(Guid transactionId);
        Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction);
        Task MarkEventAsPublishedAsync(Guid eventId);
        Task MarkEventAsInProgressAsync(Guid eventId);
        Task MarkEventAsFailedAsync(Guid eventId);
    }
}
