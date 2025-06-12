using Covid19.LocationService.Domain.Entities;

namespace Covid19.LocationService.Core.Contracts.Persistence
{
    public interface ILocationRepository : IAsyncRepository<Location>
    {

    }
}