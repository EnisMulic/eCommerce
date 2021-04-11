using Order.Domain.Common;
using System;
using System.Collections.Generic;

namespace Order.Domain
{
    public class Buyer : Entity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }
        private readonly List<PaymentMethod> paymentMethods;

        public IReadOnlyCollection<PaymentMethod> PaymentMethods => paymentMethods;

        public Buyer(Guid id, string name)
        {
            Id = Guid.Empty != id ? id : throw new ArgumentNullException(nameof(id));
            Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
        }
    }
}
