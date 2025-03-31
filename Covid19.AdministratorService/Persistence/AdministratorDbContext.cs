using Microsoft.EntityFrameworkCore;
using Covid19.AdministratorService.Domain.Common;
using Covid19.AdministratorService.Domain.Entities;

namespace Covid19.AdministratorService.Persistence
{
    public class AdministratorDbContext : DbContext
    {

        public AdministratorDbContext(DbContextOptions<AdministratorDbContext> options)
        : base(options)
        {

        }


        public DbSet<AdminBookingAllocation> AdminBookingAllocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextOptions).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
