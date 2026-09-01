using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ExpenseTracker.UserService.Api.Entities;

namespace ExpenseTracker.UserService.Api.EntityConfigurations
{
    public class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {

            builder.HasKey(user=> user.UserId);

            builder.Property(user=>user.UserName).HasMaxLength(100).IsRequired();

            builder.Property(user => user.Description).HasColumnType("nvarchar(max)");
            builder.Property(user => user.ProfilePicture).HasMaxLength(100).IsRequired(false);

            builder.Property(user => user.DateOfBirth).HasColumnType("datetime2");

            builder.Property(user => user.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(user => user.LastUpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(user => user.DeletedAt == null);



        }
    }
}
