using EventBus;
using System.Threading.Tasks;

namespace Product.Core.Interfaces
{
    public interface IProductIntegrationEventService
    {
        Task SaveEventAndProductContextChangesAsync(IntegrationEvent @event);
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}
