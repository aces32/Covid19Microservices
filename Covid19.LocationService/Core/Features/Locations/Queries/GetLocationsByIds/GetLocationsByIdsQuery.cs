using Covid19.LocationService.Core.Dto;
using MediatR;

namespace Covid19.LocationService.Core.Features.Locations.Queries.GetLocationsByIds
{
    public record GetLocationsByIdsQuery : IRequest<List<AvailableLocationDto>>
    {
        public required IEnumerable<int> LocationIds { get; set; }
    }
}
