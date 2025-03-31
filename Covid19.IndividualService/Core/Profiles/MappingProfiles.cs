using AutoMapper;
using Covid19.GrpcContracts;
using Covid19.IndividualService.Core.Features.Command.BookCovidTest;
using Covid19.IndividualService.Core.Features.Command.CancelBookedTest;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.EventContracts;

namespace Covid19.IndividualService.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // source => target
            CreateMap<Individual, BookCovidTestCommandResponse>()
                .ForMember(dest => dest.IndividualFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.IndividualLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IndividualEmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.IndividualMobileNumber, opt => opt.MapFrom(src => src.MobileNumber));

            CreateMap<BookingAvailabilityDto, BookingAvailability>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.Parse(src.BookingDate)));

            CreateMap<BookCovidTestCommand, Individual>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.IndividualFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.IndividualLastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.IndividualEmailAddress))
                .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.IndividualMobileNumber))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(_ => true));

            CreateMap<CancelBookedTestCommand, Individual>()
                    .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.IndividualEmailAddress))
                    .ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.IndividualMobileNumber))
                    .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.IndividualBookingStatus));

            CreateMap<Individual, CancelBookedTestCommandResponse>()
                    .ForMember(dest => dest.IndividualEmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                    .ForMember(dest => dest.IndividualMobileNumber, opt => opt.MapFrom(src => src.MobileNumber));


        }
    }
}
