using Covid19.AdministratorService.Domain.Entities;

namespace Covid19.AdministratorService.Persistence.Repository
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AdministratorDbContext>();
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context), "AppDbContext is not registered in the service provider.");
                }
                SeedData(context);
            }
        }
        private static void SeedData(AdministratorDbContext context)
        {
            if (!context.AdminBookingAllocations.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.AdminBookingAllocations.AddRange(
                    new AdminBookingAllocation
                    {
                        AdminBookingAllocationId = 1,
                        Capacity = 100,
                        SpaceAllocated = 30,
                        BookingDate = DateTimeOffset.UtcNow.AddDays(1),
                        LocationId = 1
                    },
                    new AdminBookingAllocation
                    {
                        AdminBookingAllocationId = 2,
                        Capacity = 80,
                        SpaceAllocated = 20,
                        BookingDate = DateTimeOffset.UtcNow.AddDays(2),
                        LocationId = 2
                    },
                    new AdminBookingAllocation
                    {
                        AdminBookingAllocationId = 3,
                        Capacity = 50,
                        SpaceAllocated = 0,
                        BookingDate = DateTimeOffset.UtcNow.AddDays(3),
                        LocationId = 1
                    }

                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}
