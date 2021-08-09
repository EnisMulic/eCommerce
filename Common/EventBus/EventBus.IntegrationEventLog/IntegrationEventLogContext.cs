using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventBus.IntegrationEventLog
{
    public class IntegrationEventLogContext : DbContext
    {
        public IntegrationEventLogContext(DbContextOptions<IntegrationEventLogContext> options) : base(options)
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
        }

        private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.ToTable("IntegrationEventLog");

            builder.HasKey(i => i.EventId);

            builder.Property(i => i.EventId)
                .IsRequired();

            builder.Property(i => i.CreatedAt)
                .IsRequired();

            builder.Property(i => i.Content)
                .IsRequired();

            builder.Property(i => i.State)
                .IsRequired();

            builder.Property(i => i.TimesSent)
                .IsRequired();

            builder.Property(i => i.EventTypeName)
                .IsRequired();
        }
    }
}
