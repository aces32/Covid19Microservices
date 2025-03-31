using Microsoft.EntityFrameworkCore;
using Covid19.IndividualService.Domain.Entities;
using Covid19.LocationService.Domain.Common;

namespace Covid19.LocationService.Persistence
{
    public class LocationDbContext : DbContext
    {

        public LocationDbContext(DbContextOptions<LocationDbContext> options)
        : base(options)
        {

        }


        public DbSet<Location> Locations { get; set; }

    }
}
