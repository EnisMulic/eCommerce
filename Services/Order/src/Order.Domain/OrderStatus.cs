using Order.Domain.Common;
using System;

namespace Order.Domain
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new(Guid.NewGuid(), nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new(Guid.NewGuid(), nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new(Guid.NewGuid(), nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Paid = new(Guid.NewGuid(), nameof(Paid).ToLowerInvariant());
        public static OrderStatus Shipped = new(Guid.NewGuid(), nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new(Guid.NewGuid(), nameof(Cancelled).ToLowerInvariant());
        public OrderStatus(Guid id, string name) : base(id, name)
        {
        }
    }
}
