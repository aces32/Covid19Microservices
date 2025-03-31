using Covid19.AdministratorService.Core.Contracts.Infrastructure.Cache;
using Covid19.AdministratorService.Models.LocationModels;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler
{

    public class GetLocationsByIdsQuery : IRequest<List<AvailableLocationListDto>>, ICachableMediatrQuery
    {
        public IEnumerable<int> LocationIds { get; init; } = [];

        public string CacheKey => $"location_ids:{string.Join(",", LocationIds.OrderBy(x => x))}";
        public bool BypassCache { get; init; } = false;
        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(60);
    }

}
