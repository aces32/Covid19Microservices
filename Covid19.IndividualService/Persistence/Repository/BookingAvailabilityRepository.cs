using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Persistence.Repository
{
    public class BookingAvailabilityRepository : BaseRepository<BookingAvailability>, IBookingAvailabilityRepository
    {
        public BookingAvailabilityRepository(IndividualDbContext dbContext) : base(dbContext)
        {
        }
    }
}
