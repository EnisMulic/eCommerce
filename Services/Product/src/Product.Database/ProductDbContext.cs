using Microsoft.EntityFrameworkCore;
using Product.Database.Configuration;

namespace Product.Database
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Domain.Product> Products { get; set; }
        public DbSet<Domain.Picture> Pictures { get; set; }
        public DbSet<Domain.Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
