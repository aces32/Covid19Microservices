using AutoMapper;
using Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome;
using Covid19.IndividualLabsService.Domain.Entities;

namespace Covid19.IndividualLabsService.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<SetTestOutcomeCommandResponse, IndividualLab>().ReverseMap();
            CreateMap<SetTestOutcomeCommand, IndividualLab>().ReverseMap();

        }
    }
}
