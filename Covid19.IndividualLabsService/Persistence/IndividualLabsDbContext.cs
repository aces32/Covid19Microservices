using Microsoft.EntityFrameworkCore;
using Covid19.IndividualLabsService.Domain.Common;
using Covid19.IndividualLabsService.Domain.Entities;

namespace Covid19.IndividualLabsService.Persistence
{
    public class IndividualLabsDbContext : DbContext
    {

        public IndividualLabsDbContext(DbContextOptions<IndividualLabsDbContext> options)
        : base(options)
        {

        }


        public DbSet<IndividualLab> individualLabs { get; set; }

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
