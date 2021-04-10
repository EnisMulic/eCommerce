using Order.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

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
            paymentMethods = new List<PaymentMethod>();
        }

        public PaymentMethod VerifyOrAddPaymentMethod(
            int cardTypeId, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            var existingPayment = paymentMethods
                .SingleOrDefault(p => p.IsEqualTo(cardTypeId, cardNumber, expiration));

            if (existingPayment != null)
            {
                return existingPayment;
            }

            var payment = new PaymentMethod(cardTypeId, cardNumber, securityNumber, cardHolderName, expiration);

            paymentMethods.Add(payment);

            return payment;
        }
    }
}
