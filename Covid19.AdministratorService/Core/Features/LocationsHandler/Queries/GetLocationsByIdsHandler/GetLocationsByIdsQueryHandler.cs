using Covid19.AdministratorService.Core.Contracts.Infrastructure;
using Covid19.AdministratorService.Models.LocationModels;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.LocationsHandler.Queries.GetLocationsByIdsHandler
{
    public class GetLocationsByIdsQueryHandler : IRequestHandler<GetLocationsByIdsQuery, List<AvailableLocationListDto>>
    {
        private readonly ILocationServiceClient _locationServiceClient;

        public GetLocationsByIdsQueryHandler(ILocationServiceClient locationServiceClient)
        {
            _locationServiceClient = locationServiceClient;
        }

        public async Task<List<AvailableLocationListDto>> Handle(GetLocationsByIdsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationServiceClient.GetLocationsByIdsAsync(request.LocationIds);
            return locations;
        }
    }
}
