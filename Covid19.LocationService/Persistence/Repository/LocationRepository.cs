using Covid19.LocationService.Domain.Entities;
using Covid19.LocationService.Core.Contracts.Persistence;

namespace Covid19.LocationService.Persistence.Repository
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(LocationDbContext dbContext) : base(dbContext)
        {

        }

    }
}
