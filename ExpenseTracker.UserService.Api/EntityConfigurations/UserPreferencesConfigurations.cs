using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseTracker.UserService.Api.Entities;

namespace ExpenseTracker.UserService.Api.EntityConfigurations
{
    public class UserPreferencesConfigurations: IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            builder.HasKey(pref => pref.UserId);
            builder.Property(pref=>pref.UserId).ValueGeneratedNever();
            builder.Property(pref=>pref.DefaultCurrency).IsRequired();
            builder.Property(pref=>pref.TimeZone).IsRequired();

            builder.Property(pref=>pref.DateFormat).HasConversion<string>().HasMaxLength(10).IsRequired();

            builder.Property(pref=>pref.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(pref=>pref.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(pref=>pref.UserProfile).WithOne().HasForeignKey<UserPreferences>(pref => pref.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
