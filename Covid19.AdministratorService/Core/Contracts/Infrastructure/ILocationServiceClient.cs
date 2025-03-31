using Covid19.AdministratorService.Models.LocationModels;

namespace Covid19.AdministratorService.Core.Contracts.Infrastructure
{
    public interface ILocationServiceClient
    {
        Task<List<AvailableLocationListDto>> GetAllAvailableLocationsAsync();
        Task<List<AvailableLocationListDto>> GetLocationsByIdsAsync(IEnumerable<int> ids);

    }
}
