using System.Collections.Generic;

namespace Basket.Contracts.Responses
{
    public class CustomerBasketResponse
    {
        public List<BasketItemResponse> Items { get; set; } = new();
    }
}
