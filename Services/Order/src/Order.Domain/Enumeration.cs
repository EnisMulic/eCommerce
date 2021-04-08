using System;

namespace Order.Domain
{
    public abstract class Enumeration : IComparable
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Enumeration)obj).Id);
        }
    }
}
