using Microsoft.EntityFrameworkCore;
using ExpenseTracker.DeviceConsentService.Api.Entities;

namespace ExpenseTracker.DeviceConsentService.Api.Data
{
    public class DeviceConsentDbContext(DbContextOptions<DeviceConsentDbContext> options) : DbContext(options)
    {
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<ConnectedSource> ConnectedSources => Set<ConnectedSource>();
        public DbSet<Consent> Consents => Set<Consent>();
        public DbSet<SyncSession> SyncSessions => Set<SyncSession>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceConsentDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is Device device)
                {
                    if (entry.State == EntityState.Added)
                    {
                        device.CreatedAt = DateTime.UtcNow;
                        device.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        device.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        device.DeletedAt = DateTime.UtcNow;
                        device.LastUpdatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.Entity is ConnectedSource source)
                {
                    if (entry.State == EntityState.Added)
                    {
                        source.CreatedAt = DateTime.UtcNow;
                        source.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        source.LastUpdatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.Entity is Consent consent)
                {
                    if (entry.State == EntityState.Added)
                    {
                        consent.CreatedAt = DateTime.UtcNow;
                        consent.LastUpdatedAt = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        consent.LastUpdatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.Entity is SyncSession syncSession)
                {
                    if (entry.State == EntityState.Added)
                    {
                        syncSession.CreatedAt = DateTime.UtcNow;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
