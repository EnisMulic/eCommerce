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

        public PaymentMethod(int cardTypeId, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration)
        {
            this.cardNumber = !string.IsNullOrWhiteSpace(cardNumber) ? cardNumber : throw new ArgumentException(nameof(cardNumber));
            this.securityNumber = !string.IsNullOrWhiteSpace(securityNumber) ? securityNumber : throw new ArgumentException(nameof(securityNumber));
            this.cardHolderName = !string.IsNullOrWhiteSpace(cardHolderName) ? cardHolderName : throw new ArgumentException(nameof(cardHolderName));
            this.expiration = expiration < DateTime.UtcNow ? expiration : throw new ArgumentException(nameof(expiration));
            this.cardTypeId = cardTypeId;
        }

        public bool IsEqualTo(int cardTypeId, string cardNumber, DateTime expiration)
        {
            return this.cardTypeId == cardTypeId && 
                   this.cardNumber == cardNumber && 
                   this.expiration == expiration;
        }
    }
}
