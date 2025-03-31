using AutoMapper;
using Covid19.LocationService.Core.Contracts.Persistence;
using Covid19.LocationService.Core.Dto;
using MediatR;

namespace Covid19.LocationService.Core.Features.Locations.Queries.GetAllAvailableLocations
{
    public class GetAllAvailableLocationsQueryHandler : IRequestHandler<GetAllAvailableLocationsQuery, List<AvailableLocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<GetAllAvailableLocationsQueryHandler> _logger;

        public GetAllAvailableLocationsQueryHandler(IMapper mapper, ILocationRepository locationRepository,
            ILogger<GetAllAvailableLocationsQueryHandler> logger)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public async Task<List<AvailableLocationDto>> Handle(GetAllAvailableLocationsQuery request, CancellationToken cancellationToken)
        {
            var allLocations = (await _locationRepository.ListAllAsync()).OrderBy(x => x.LocationID);
            return _mapper.Map<List<AvailableLocationDto>>(allLocations);
        }
    }
}
