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
    class ProductOptionCombinationConfiguration : IEntityTypeConfiguration<ProductOptionCombination>
    {
        public void Configure(EntityTypeBuilder<ProductOptionCombination> builder)
        {
            builder.HasKey(b => new { b.ProductId, b.ProductOptionId });
        }
    }
}
