using AutoMapper;
using Covid19.LocationService.Core.Contracts.Persistence;
using Covid19.LocationService.Core.Dto;
using Covid19.LocationService.Core.Features.Locations.Queries.GetAllAvailableLocations;
using MediatR;

namespace Covid19.LocationService.Core.Features.Locations.Queries.GetLocationsByIds
{
    public class GetLocationsByIdsQueryHandler : IRequestHandler<GetLocationsByIdsQuery, List<AvailableLocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<GetAllAvailableLocationsQueryHandler> _logger;

        public GetLocationsByIdsQueryHandler(IMapper mapper, ILocationRepository locationRepository,
            ILogger<GetAllAvailableLocationsQueryHandler> logger)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public async Task<List<AvailableLocationDto>> Handle(GetLocationsByIdsQuery request, CancellationToken cancellationToken)
        {
            var allLocations = await _locationRepository.WhereAsync(l => request.LocationIds.Contains(l.LocationID));
            return _mapper.Map<List<AvailableLocationDto>>(allLocations);
        }
    }
}
