using Order.Domain.Common;
using System;

namespace Order.Domain
{
    public class OrderItem : Entity<Guid>
    {
        private string productName;
        private string productImage;
        private decimal unitPrice;
        private decimal discount;
        private int units;

        public Guid ProductId { get; internal set; }

        public OrderItem()
        {
        }

        public OrderItem(Guid productId, string productName, string productImage, decimal unitPrice, decimal discount, int units = 1)
        {
            ProductId = productId;
            this.productName = productName;
            this.productImage = productImage;
            this.unitPrice = unitPrice;
            this.discount = discount;
            this.units = units;
        }

        public string GetImage() => productImage;
        public string GetProductName() => productName;
        public int GetUnits() => units;
        public decimal GetUnitPrice() => unitPrice;
        public decimal GetDiscount() => discount;

        public void SetDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new ArgumentException("Discount is not valid");
            }

            this.discount = discount;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
            {
                throw new ArgumentException("Invalid units");
            }

            this.units += units;
        }
    }
}
