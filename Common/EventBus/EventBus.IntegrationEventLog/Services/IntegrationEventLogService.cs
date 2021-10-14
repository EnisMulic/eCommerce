using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EventBus.IntegrationEventLog.Services
{
    public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
    {
        private readonly IntegrationEventLogContext _context;
        private readonly DbConnection _dbConnection;
        private readonly List<Type> _eventTypes;
        private volatile bool disposedValue;

        public IntegrationEventLogService(DbConnection dbConnection)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));

            _context = new IntegrationEventLogContext(
                new DbContextOptionsBuilder<IntegrationEventLogContext>()
                    .UseNpgsql(_dbConnection).Options);

            _eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
                .GetTypes()
                .Where(i => i.Name.EndsWith(nameof(IntegrationEvent)))
                .ToList();
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> GetEventsPendingToPublishAsync(Guid transactionId)
        {
            var events = await _context.IntegrationEventLogs
                .Where(i => i.TransactionId == transactionId && i.State == EventState.NotPublished)
                .ToListAsync();

            if (events != null && events.Any())
            {
                return events.OrderBy(i => i.CreatedAt)
                    .Select(i => i.DeserializeJsonContent(_eventTypes.Find(j => j.Name == i.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

            var tr = transaction.GetDbTransaction();
            _context.Database.UseTransaction(tr);
            _context.IntegrationEventLogs.AddAsync(eventLogEntry);

            return _context.SaveChangesAsync();
        }

        public Task MarkEventAsFailedAsync(Guid eventId)
        {
            return UpdateEventState(eventId, EventState.PublishFailed);
        }

        public Task MarkEventAsInProgressAsync(Guid eventId)
        {
            return UpdateEventState(eventId, EventState.InProgress);
        }

        public Task MarkEventAsPublishedAsync(Guid eventId)
        {
            return UpdateEventState(eventId, EventState.Published);
        }

        private Task UpdateEventState(Guid eventId, EventState state)
        {
            var @event = _context.IntegrationEventLogs.Single(i => i.EventId == eventId);
            @event.State = state;

            if (state == EventState.InProgress)
            {
                @event.TimesSent++;
            }

            _context.IntegrationEventLogs.Update(@event);
            return _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }


                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
