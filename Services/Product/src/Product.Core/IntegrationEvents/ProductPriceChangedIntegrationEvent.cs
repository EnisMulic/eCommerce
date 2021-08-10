using EventBus;
using System;

namespace Product.Core.IntegrationEvents
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
}
