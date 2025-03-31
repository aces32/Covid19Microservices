using Covid19.IndividualService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Covid19.LocationService.Persistence.Repository
{
    public static class PrepDb
    {
        public static void PrepPopulation(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<LocationDbContext>();
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context), "AppDbContext is not registered in the service provider.");
                }
                SeedData(context);
            }
        }
        private static void SeedData(LocationDbContext context)
        {
            if (!context.Locations.Any())
            {
                Console.WriteLine("--> Seeding data...");
                context.Locations.AddRange(
                    new Location{ LocationID = 1, LocationName = "Lagos, Nigeria" },
                    new Location{ LocationID = 2, LocationName = "Munich, Germany" },
                    new Location{ LocationID = 3, LocationName = "Berlin, Germany" },
                    new Location{ LocationID = 4, LocationName = "Oyo, Nigeria" }

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
