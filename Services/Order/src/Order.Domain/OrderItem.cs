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
        public OrderItem()
        {
        }

        public OrderItem(string productName, string productImage, decimal unitPrice, decimal discount, int units = 1)
        {
            this.productName = productName;
            this.productImage = productImage;
            this.unitPrice = unitPrice;
            this.discount = discount;
            this.units = units;
        }
    }
}
