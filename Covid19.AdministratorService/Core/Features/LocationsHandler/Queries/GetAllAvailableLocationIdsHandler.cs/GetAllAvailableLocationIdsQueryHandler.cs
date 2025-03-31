using Covid19.AdministratorService.Core.Contracts.Infrastructure;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetAllAvailableLocationIdsHandler.cs
{
    public class GetAllAvailableLocationIdsQueryHandler : IRequestHandler<GetAllAvailableLocationIdsQuery, List<int>>
    {
        private readonly ILocationServiceClient _locationService;

        public GetAllAvailableLocationIdsQueryHandler(ILocationServiceClient locationService)
        {
            _locationService = locationService;
        }

        public async Task<List<int>> Handle(GetAllAvailableLocationIdsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetAllAvailableLocationsAsync(); 
            return [.. locations.Select(l => l.LocationID)];
        }
    }

}
