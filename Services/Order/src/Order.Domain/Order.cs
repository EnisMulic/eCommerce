using Order.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void SetPaymentId(Guid id)
        {
            paymentMethodId = id;
        }

        public void SetBuyerId(Guid id)
        {
            buyerId = id;
        }

        public void AddOrderItem(Guid id, string productName, string productImage, decimal unitPrice, decimal discount, int units = 1)
        {
            var existingOrderItem = orderItems.Where(o => o.ProductId == id)
                .SingleOrDefault();

            if (existingOrderItem != null)
            {
                if (discount > existingOrderItem.GetDiscount())
                {
                    existingOrderItem.SetDiscount(discount);
                }

                existingOrderItem.AddUnits(units);
            }
            else
            {
                var orderItem = new OrderItem(id, productName, productImage, unitPrice, discount, units);
                orderItems.Add(orderItem);
            }
        }

        public void SetAwaitingValidationStatus()
        {
            if (orderStatusId == OrderStatus.Submitted.Id)
            {
                orderStatusId = OrderStatus.AwaitingValidation.Id;
            }
        }

        public void SetStockConfirmedStatus()
        {
            if (orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                orderStatusId = OrderStatus.StockConfirmed.Id;
            }
        }

        public void SetPaidStatus()
        {
            if (orderStatusId == OrderStatus.StockConfirmed.Id)
            {
                orderStatusId = OrderStatus.Paid.Id;
            }
        }

        public void SetShippedStatus()
        {
            if (orderStatusId == OrderStatus.Paid.Id)
            {
                orderStatusId = OrderStatus.Shipped.Id;
            }
        }

        public void SetCancelledStatus()
        {
            if (orderStatusId == OrderStatus.Paid.Id || orderStatusId == OrderStatus.Shipped.Id)
            {
                throw new Exception("Order cannot be canceled");
            }

            orderStatusId = OrderStatus.Cancelled.Id;
        }

        public decimal GetTotal()
        {
            return orderItems.Sum(o => o.GetUnits() * o.GetUnitPrice());
        }
    }
}
