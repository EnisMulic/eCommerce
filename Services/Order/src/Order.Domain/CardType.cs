using System;

namespace Order.Domain
{
    public class CardType : Enumeration
    {
        public static CardType Visa = new(Guid.NewGuid(), nameof(Visa));
        public static CardType MasterCard = new(Guid.NewGuid(), nameof(MasterCard));
        protected CardType(Guid id, string name) : base(id, name)
        {
        }
    }
}
