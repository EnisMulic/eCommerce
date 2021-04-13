using System;
using System.Collections.Generic;

namespace Basket.Contracts.Requests
{
    public class CustomerBasketUpdateRequest
    {
        public List<BasketItem> Items { get; set; }

        public class BasketItem
        {
            public Guid ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public string ImageUrl { get; set; }
        }
    }
}
