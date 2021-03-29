using System;

namespace Product.Domain
{
    public class ProductOption : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
