using ExpenseTracker.DeviceConsentService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.DeviceConsentService.Api.EntityConfigurations
{
    public class SyncSessionConfigurations : IEntityTypeConfiguration<SyncSession>
    {
        public void Configure(EntityTypeBuilder<SyncSession> builder)
        {
            builder.HasKey(sync => sync.Id);

            builder.Property(sync => sync.Id).HasDefaultValueSql("NEWID()");

            builder.Property(sync => sync.Status).IsRequired();

            builder.Property(sync => sync.StartedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(sync => sync.LastSyncedWaterMark).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(sync => sync.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
