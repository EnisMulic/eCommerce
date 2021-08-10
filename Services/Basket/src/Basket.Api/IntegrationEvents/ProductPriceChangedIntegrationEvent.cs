using Basket.Api.Repository;
using EventBus;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.IntegrationEvents
{
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public Guid ProductId { get; private init; }

        public decimal NewPrice { get; private init; }

        public decimal OldPrice { get; private init; }

        public ProductPriceChangedIntegrationEvent(Guid productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }

    public class ProductPriceChangedIntegrationEventHandler : IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly ILogger<ProductPriceChangedIntegrationEventHandler> _logger;
        private readonly IBasketRepository _repository;

        public ProductPriceChangedIntegrationEventHandler(
            ILogger<ProductPriceChangedIntegrationEventHandler> logger, 
            IBasketRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-Basket-Api"))
            {
                _logger.LogInformation($"Handling integration event: {@event.Id} at Basket-Api - ({@event})");

                var userIds = _repository.GetUsers();

                foreach (var id in userIds)
                {
                    await UpdatePriceInBasketItems(id, @event);
                }
            }
        }

        private async Task UpdatePriceInBasketItems(string basketId, ProductPriceChangedIntegrationEvent @event)
        {
            var basket = await _repository.GetBasketAsync(basketId);

            var itemsToUpdate = basket?.Items?.Where(x => x.ProductId == @event.ProductId).ToList();

            if (itemsToUpdate != null)
            {
                _logger.LogInformation($"ProductPriceChangedIntegrationEventHandler - Updating items in basket for user: {basket.CustomerId} ({itemsToUpdate})");

                foreach (var item in itemsToUpdate)
                {
                    if (item.UnitPrice == @event.OldPrice)
                    {
                        var originalPrice = item.UnitPrice;
                        item.UnitPrice = @event.NewPrice;
                        item.OldUnitPrice = originalPrice;
                    }
                }
                await _repository.UpdateBasketAsync(basket);
            }
        }
    }
}
