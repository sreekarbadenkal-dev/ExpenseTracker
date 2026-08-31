using Microsoft.EntityFrameworkCore;
using ExpenseTracker.UserService.Api.Entities;
    
namespace ExpenseTracker.UserService.Api.Data
{
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {
        public DbSet<UserProfile> Users => Set<UserProfile>();
        public DbSet<UserPreferences> Preferences => Set<UserPreferences>();



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if(entry.Entity is UserProfile profile)
                {
                    if (entry.State == EntityState.Added)
                    {
                        profile.CreatedAt = DateTime.UtcNow;
                        profile.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        profile.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        profile.DeletedAt = DateTime.UtcNow;
                        profile.LastUpdatedAt = DateTime.UtcNow;
                    }
                }

                if(entry.Entity is UserPreferences preferences)
                {

                    if (entry.State == EntityState.Added)
                    {
                        preferences.CreatedAt = DateTime.UtcNow;
                        preferences.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        preferences.LastUpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
