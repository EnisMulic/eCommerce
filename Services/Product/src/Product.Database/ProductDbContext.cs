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
        public DbSet<Domain.Image> Images { get; set; }
        public DbSet<Domain.Category> Categories { get; set; }
        public DbSet<Domain.ProductCategory> ProductCategories { get; set; }
        public DbSet<Domain.ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Domain.ProductAttributeGroup> ProductAttributeGroups { get; set; }
        public DbSet<Domain.ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<Domain.ProductOption> ProductOptions { get; set; }
        public DbSet<Domain.ProductOptionCombination> ProductOptionCombinations { get; set; }
        public DbSet<Domain.ProductAttributeValue> ProductAttributeValue { get; set; }
        public DbSet<Domain.ProductOptionValue> ProductOptionValues { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductOptionCombinationConfiguration());
        }
    }
}
