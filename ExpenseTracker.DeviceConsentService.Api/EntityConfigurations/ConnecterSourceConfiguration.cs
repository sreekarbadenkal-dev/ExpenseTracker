using ExpenseTracker.DeviceConsentService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.DeviceConsentService.Api.EntityConfigurations
{
    public class ConnecterSourceConfiguration : IEntityTypeConfiguration<ConnectedSource>
    {
        public void Configure(EntityTypeBuilder<ConnectedSource> builder)
        {
            builder.HasKey(sor => sor.Id);

            builder.Property(sor => sor.Id).HasDefaultValueSql("NEWID()");

            builder.Property(sor => sor.SourceType).IsRequired();

            builder.HasIndex(sor => new { sor.DeviceId, sor.SourceIdentifier })
                .IsUnique()
                .HasFilter("[SourceIdentifier] IS NOT NULL");

            builder.Property(sor => sor.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(sor => sor.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(sor=>sor.Consents)
                .WithOne(con=>con.ConnectedSource)
                .HasForeignKey(con=>con.ConnectedSourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(sor=>sor.SyncSessions)
                .WithOne(syn=>syn.ConnectedSource)
                .HasForeignKey(syn=>syn.ConnectedSourceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
