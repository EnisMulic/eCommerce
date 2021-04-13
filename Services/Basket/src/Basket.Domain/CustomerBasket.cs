using System;
using System.Collections.Generic;

namespace Basket.Domain
{
    public class CustomerBasket
    {
        public Guid CustomerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
    }
}
