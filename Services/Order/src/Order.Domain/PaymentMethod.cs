using System;

namespace Order.Domain
{
    public class PaymentMethod : Entity<Guid>
    {
        private string cardNumber;
        private string securityNumber;
        private string cardHolderName;
        private DateTime expiration;

        private Guid cardTypeId;
        public CardType CardType { get; private set; }

        protected PaymentMethod()
        {
        }

        public PaymentMethod(string cardNumber, string securityNumber, string cardHolderName, DateTime expiration, Guid cardTypeId)
        {
            this.cardNumber = cardNumber;
            this.securityNumber = securityNumber;
            this.cardHolderName = cardHolderName;
            this.expiration = expiration;
            this.cardTypeId = cardTypeId;
        }
    }
}
