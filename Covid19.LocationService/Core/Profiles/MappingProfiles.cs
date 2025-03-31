using AutoMapper;
using Covid19.IndividualService.Domain.Entities;
using Covid19.LocationService.Core.Dto;
using Covid19.LocationService.Core.Features.Locations.Queries.GetLocationsByIds;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Covid19.LocationService.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // source => target
            CreateMap<Location, AvailableLocationDto>();
        }
    }
}
