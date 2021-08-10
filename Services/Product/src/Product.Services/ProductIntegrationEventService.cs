using EventBus;
using EventBus.IntegrationEventLog.Services;
using EventBus.IntegrationEventLog.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product.Core.Interfaces;
using Product.Database;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductIntegrationEventService : IProductIntegrationEventService, IDisposable
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly ProductDbContext _context;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<ProductIntegrationEventService> _logger;
        private volatile bool disposedValue;

        public ProductIntegrationEventService(
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, 
            IEventBus eventBus, 
            ProductDbContext context, 
            ILogger<ProductIntegrationEventService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation($"Publishing integration event: {@event.Id} from \"Product-Api\" - ({@event})");

                await _eventLogService.MarkEventAsInProgressAsync(@event.Id);
                _eventBus.Publish(@event);
                await _eventLogService.MarkEventAsPublishedAsync(@event.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR Publishing integration event: {@event.Id} from \"Product-Api\" - ({@event})");
                await _eventLogService.MarkEventAsFailedAsync(@event.Id);
            }
        }

        public async Task SaveEventAndProductContextChangesAsync(IntegrationEvent @event)
        {
            _logger.LogInformation($"ProductIntegrationEventService - Saving changes and integrationEvent: {@event.Id}");
          
            await ResilientTransaction.New(_context).ExecuteAsync(async () =>
            {
                await _context.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(@event, _context.Database.CurrentTransaction);
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (_eventLogService as IDisposable)?.Dispose();
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
