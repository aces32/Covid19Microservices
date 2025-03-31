using Covid19.LocationService.Core.Dto;
using MediatR;

namespace Covid19.LocationService.Core.Features.Locations.Queries.GetAllAvailableLocations
{
    public class GetAllAvailableLocationsQuery : IRequest<List<AvailableLocationDto>>
    {
    }
}
