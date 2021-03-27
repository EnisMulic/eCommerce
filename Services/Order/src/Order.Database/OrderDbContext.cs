using Microsoft.EntityFrameworkCore;

namespace Order.Database
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
    }
}
