using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Core.Contracts.Persistence
{
    public interface IBookingAvailabilityRepository : IAsyncRepository<BookingAvailability>
    {
    }
}
