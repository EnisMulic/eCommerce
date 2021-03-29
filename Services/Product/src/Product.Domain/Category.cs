using System;
using System.Collections.Generic;

namespace Product.Domain
{
    public class Category : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
