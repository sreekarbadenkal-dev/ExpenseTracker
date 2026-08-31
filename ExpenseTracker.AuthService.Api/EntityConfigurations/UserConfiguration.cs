using ExpenseTracker.AuthService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.AuthService.Api.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(user => user.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(user => user.DeletedAt == null);
            builder.Property(user => user.PhoneNumber).HasMaxLength(15).IsRequired(false);
        }
    }
}
