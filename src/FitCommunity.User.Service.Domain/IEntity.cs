using System;
using System.Collections.Generic;
using System.Text;

namespace FitCommunity.User.Service.Domain
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
