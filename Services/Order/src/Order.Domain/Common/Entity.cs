namespace Order.Domain.Common
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
