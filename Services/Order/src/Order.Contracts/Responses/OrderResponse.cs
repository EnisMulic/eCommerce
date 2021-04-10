using System;

namespace Order.Contracts.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
