using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Database.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<Domain.ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(b => new { b.CategoryId, b.ProductId});
        }
    }
}
