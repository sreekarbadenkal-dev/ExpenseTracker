using Microsoft.EntityFrameworkCore;
using ExpenseTracker.AuthService.Api.Entities;
    
namespace ExpenseTracker.AuthService.Api.Data
{
    public class AuthServiceDbContext : DbContext
    {
        public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext>
            options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<User>();

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
