using System;

namespace Order.Domain
{
    public class Buyer : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }

        public Buyer(Guid id, string name)
        {
            Id = Guid.Empty != id ? id : throw new ArgumentNullException(nameof(id));
            Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
        }
    }
}
