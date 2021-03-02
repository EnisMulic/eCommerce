using System;

namespace FitCommunity.User.Service.Domain
{
    public class Role : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
    }
}
