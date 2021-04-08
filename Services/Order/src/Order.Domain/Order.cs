using System;

namespace Order.Domain
{
    public class Order : Entity<Guid>, IAggregateRoot
    {
        public Order()
        {
        }
    }
}
