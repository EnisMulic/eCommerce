using System;
using System.Collections.Generic;

namespace Product.Domain
{
    public class ProductAttributeGroup : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
    }
}