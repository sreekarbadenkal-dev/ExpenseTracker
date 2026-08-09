using ExpenseTracker.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseTracker.Api.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(user => user.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(user => user.DeletedAt == null);
        }
    }
}
