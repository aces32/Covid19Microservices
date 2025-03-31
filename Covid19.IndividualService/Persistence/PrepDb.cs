using Covid19.IndividualService.Core.Contracts.Infrastructure;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;

namespace Covid19.IndividualService.Persistence
{
    public static class PrepDb
    {
        public static async Task PrepPopulation(this IApplicationBuilder app)
        {
            Console.WriteLine($"--> Calling gRPC Service");
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IBookingAvailabilitySyncService>();
                if (grpcClient == null)
                {
                    throw new ArgumentNullException(nameof(grpcClient));
                }
                var bookingsAvailability = await grpcClient.SyncBookingAllocationAsync()
                    ?? throw new ArgumentNullException();
                SeedData(serviceScope.ServiceProvider.GetService<IBookingAvailabilityRepository>(), bookingsAvailability);
            }
        }

        private static async void SeedData(IBookingAvailabilityRepository? bookingAvailabilityRepository,
            IEnumerable<BookingAvailability> bookingAvailabilities)
        {
            ArgumentNullException.ThrowIfNull(bookingAvailabilityRepository);

            Console.WriteLine("--> Seeding Platforms...");
            foreach (var dto in bookingAvailabilities)
            {
                var start = dto.BookingDate;
                var end = start.AddDays(1);
                var bookingexists = await bookingAvailabilityRepository.AnyAsync(x =>
                                                                    x.LocationId == dto.LocationId &&
                                                                    x.BookingDate >= start &&
                                                                    x.BookingDate < end);
                if (bookingexists)
                {
                    continue;
                }

                var projection = new BookingAvailability
                {
                    AdminBookingAllocationId = dto.AdminBookingAllocationId,
                    LocationId = dto.LocationId,
                    BookingDate = dto.BookingDate,
                    Capacity = dto.Capacity,
                    SpaceAllocated = dto.SpaceAllocated
                };

                await bookingAvailabilityRepository.AddAsync(projection);

            }

            Console.WriteLine($"--> Calling gRPC SyncBookingAllocationAsync added");
        }

    }
}
