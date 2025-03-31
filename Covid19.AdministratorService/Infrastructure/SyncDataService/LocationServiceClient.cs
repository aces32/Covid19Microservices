using Covid19.AdministratorService.Core.Contracts.Infrastructure;
using Covid19.AdministratorService.Models.LocationModels;

namespace Covid19.AdministratorService.Infrastructure.SyncDataService
{
    public class LocationServiceClient : ILocationServiceClient
    {
        private readonly HttpClient _httpClient;

        public LocationServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AvailableLocationListDto>> GetAllAvailableLocationsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AvailableLocationListDto>>(string.Empty);
            return response ?? [];
        }

        public async Task<List<AvailableLocationListDto>> GetLocationsByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                throw new ArgumentNullException();
            }

            var query = string.Join("&", ids.Select(id => $"locationIds={id}"));
            var url = $"GetLocationByIds?{query}";

            var response = await _httpClient.GetFromJsonAsync<List<AvailableLocationListDto>>(url);
            return response ?? [];
        }
    }
}
