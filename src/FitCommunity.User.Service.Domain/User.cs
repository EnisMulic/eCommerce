﻿using System;

namespace FitCommunity.User.Service.Domain
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
