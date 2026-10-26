using ExpenseTracker.DeviceConsentService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.DeviceConsentService.Api.EntityConfigurations
{
    public class ConsentConfigurations : IEntityTypeConfiguration<Consent>
    {
        public void Configure(EntityTypeBuilder<Consent> builder)
        {
            builder.HasKey(con => con.Id);

            builder.Property(con => con.Id).HasDefaultValueSql("NEWID()");

            builder.Property(con => con.PermissionScope).IsRequired();

            builder.Property(con => con.Status).IsRequired();

            builder.Property(con => con.GrantedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(con => con.ConsentMetadata).IsRequired(false);

            builder.Property(con => con.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(con => con.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
