using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Database.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product> builder)
        {
            
        }
    }
}
