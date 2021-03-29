using System;

namespace Product.Domain
{
    public class ProductAttribute : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AttributeGroupId { get; set; }
        public ProductAttributeGroup ProductAttributeGroup { get; set; }
    }
}
