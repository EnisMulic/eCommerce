using System;

namespace Product.Domain
{
    public class Picture : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Uri { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
