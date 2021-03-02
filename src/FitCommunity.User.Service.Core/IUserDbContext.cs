using FitCommunity.User.Service.Domain;
using Microsoft.EntityFrameworkCore;

namespace FitCommunity.User.Service.Core
{
    public interface IUserDbContext
    {
        DbSet<Domain.User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
    }
}
