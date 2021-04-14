using System;

namespace Order.Contracts.Responses
{
    public class OrderItemResponse
    {
        public Guid Id { get; set; }
        public string ProductName {get; set;}
        public string ProductImage {get; set;}
        public decimal UnitPrice {get; set;}
        public decimal Discount {get; set;}
        public int Units { get; set; }
    }
}
