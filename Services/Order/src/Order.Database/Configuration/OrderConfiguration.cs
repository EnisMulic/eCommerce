using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;
using System;

namespace Order.Database.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Order> builder)
        {
            builder.Property<Guid>("buyerId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("BuyerId")
                .IsRequired();

            builder.Property<Guid>("paymentMethodId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("PaymentMethodId")
                .IsRequired();

            builder.Property<Guid>("orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderStatusId")
                .IsRequired();

            builder.Property<DateTime>("orderDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("OrderDate")
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(Domain.Order.OrderItems))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<Buyer>()
                .WithMany()
                .HasForeignKey("buyerId")
                .IsRequired();

            builder.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("paymentMethodId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<OrderStatus>()
                .WithMany()
                .HasForeignKey("orderStatusId")
                .IsRequired(true);
        }
    }
}
