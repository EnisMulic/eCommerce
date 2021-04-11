using Order.Domain.Common;
using System;
using System.Collections.Generic;

namespace Order.Domain
{
    public class Order : Entity<Guid>, IAggregateRoot
    {
        private DateTime orderDate;
        private Guid buyerId;
        private int orderStatusId;
        private Guid paymentMethodId;
        private readonly List<OrderItem> orderItems;

        // public OrderStatus OrderStatus { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => orderItems;

        public Order()
        {
        }

        public Order(DateTime orderDate, Guid buyerId, int orderStatusId, Guid paymentMethodId)
        {
            this.orderDate = orderDate;
            this.buyerId = buyerId;
            this.orderStatusId = orderStatusId;
            this.paymentMethodId = paymentMethodId;
        }
    }
}
