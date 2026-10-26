using ExpenseTracker.DeviceConsentService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.DeviceConsentService.Api.EntityConfigurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(dev => dev.Id);

            builder.Property(dev => dev.Id).HasDefaultValueSql("NEWID()");

            builder.Property(dev => dev.DeviceIdentifier).IsRequired().HasMaxLength(100);

            builder.HasIndex(dev => new {dev.UserId,dev.DeviceIdentifier}).IsUnique();

            builder.Property(dev => dev.DeviceType).IsRequired().HasMaxLength(50);

            builder.Property(dev => dev.DeviceModel).HasMaxLength(300);

            builder.Property(dev=> dev.OsModel).IsRequired();   

            builder.Property(dev=>dev.AppVersion).IsRequired().HasMaxLength(50);

            builder.Property(dev => dev.PushNotificationToken).IsRequired(false).HasMaxLength(50);

            builder.Property(dev=>dev.IsActive).IsRequired();

            builder.Property(dev => dev.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(dev=>dev.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasMany(dev => dev.ConnectedSources)
                .WithOne(sor => sor.Device)
                .HasForeignKey(sor => sor.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}