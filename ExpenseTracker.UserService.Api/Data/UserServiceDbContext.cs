using Microsoft.EntityFrameworkCore;
using ExpenseTracker.UserService.Api.Entities;
    
namespace ExpenseTracker.UserService.Api.Data
{
    public class UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : DbContext(options)
    {
        public DbSet<UserProfile> Users => Set<UserProfile>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<UserProfile>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
