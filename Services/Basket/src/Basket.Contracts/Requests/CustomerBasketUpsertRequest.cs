using System;

namespace Basket.Contracts.Requests
{
    public class CustomerBasketUpsertRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
