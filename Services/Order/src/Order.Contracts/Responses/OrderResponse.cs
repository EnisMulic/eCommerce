using System;
using System.Collections.Generic;

namespace Order.Contracts.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatusResponse OrderStatus { get; set; }
        public Guid PaymentMethodId { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; }
    }
}
