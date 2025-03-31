using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Core.Contracts.Infrastructure
{
    public interface IBookingAvailabilitySyncService
    {
        Task<IEnumerable<BookingAvailability>> SyncBookingAllocationAsync();
    }
}
