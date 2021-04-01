using System;

namespace Product.Domain
{
    public class ProductAttribute : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProductAttributeGroupId { get; set; }
        public ProductAttributeGroup ProductAttributeGroup { get; set; }
    }
}
