﻿using FitCommunity.User.Service.Core;
using Microsoft.EntityFrameworkCore;

namespace FitCommunity.User.Service.Database
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.User> Users { get ; set ; }
    }
}