using Order.Domain.Common;
using System;

namespace Order.Domain
{
    public class PaymentMethod : Entity<Guid>
    {
        private string cardNumber;
        private string securityNumber;
        private string cardHolderName;
        private DateTime expiration;

        private int cardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        {
        }

        public PaymentMethod(string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, int cardTypeId)
        {
            this.cardNumber = cardNumber;
            this.securityNumber = securityNumber;
            this.cardHolderName = cardHolderName;
            this.expiration = expiration;
            this.cardTypeId = cardTypeId;
        }
    }
}
