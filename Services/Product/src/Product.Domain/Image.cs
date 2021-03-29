using System;

namespace Product.Domain
{
    public class Image : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Uri { get; set; }
    }
}
