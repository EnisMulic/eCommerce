using System;

namespace Product.Domain
{
    public class Category : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
    }
}
