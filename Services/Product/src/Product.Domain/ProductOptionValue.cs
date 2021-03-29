using System;

namespace Product.Domain
{
    public class ProductOptionValue : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductOptionId { get; set; }
        public ProductOption ProductOption { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Value { get; set; }
        public string DisplayType { get; set; }
    }
}
