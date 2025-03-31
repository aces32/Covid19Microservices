using Covid19.AdministratorService.Core.Contracts.Infrastructure.Cache;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetAllAvailableLocationIdsHandler.cs
{
    public record GetAllAvailableLocationIdsQuery() : IRequest<List<int>>, ICachableMediatrQuery
    {
        public string CacheKey => "location_ids";
        public bool BypassCache { get; init; } = false;
        public TimeSpan? SlidingExpiration => TimeSpan.FromMinutes(60); 
    }

}
