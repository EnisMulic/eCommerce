using Order.Domain.Common;

namespace Order.Domain
{
    public class CardType : Enumeration
    {
        public static CardType Visa = new(1, nameof(Visa));
        public static CardType MasterCard = new(2, nameof(MasterCard));
        protected CardType(int id, string name) : base(id, name)
        {
        }
    }
}
