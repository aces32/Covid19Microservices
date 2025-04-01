using AutoMapper;
using Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace;
using Covid19.AdministratorService.Domain.Entities;
using Covid19.Shared.EventContracts;

namespace Covid19.AdministratorService.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AllocateBookingSpaceCommand, AdminBookingAllocation>().ReverseMap();
            CreateMap<AllocateBookingSpaceCommandResponse, AdminBookingAllocation>().ReverseMap();
            CreateMap<AdminBookingAllocation, BookingSlotCreatedEvent>();
        }
    }
}
