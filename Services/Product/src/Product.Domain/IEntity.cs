﻿namespace Product.Domain
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
