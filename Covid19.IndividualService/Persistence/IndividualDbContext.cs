using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Covid19.IndividualService.Domain.Common;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Persistence
{
    public class IndividualDbContext : DbContext
    {

        public IndividualDbContext(DbContextOptions<IndividualDbContext> options)
        : base(options)
        {

        }


        public DbSet<Individual> Individuals { get; set; }
        public DbSet<BookingAvailability> BookingAvailabilities { get; set; }

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
