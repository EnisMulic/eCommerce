using System;

namespace Product.Domain
{
    public class ProductAttributeValue : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ProductAttributeId { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
    }
}
