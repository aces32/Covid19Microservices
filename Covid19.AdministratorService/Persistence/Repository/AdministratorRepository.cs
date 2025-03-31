using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Domain.Entities;
using Covid19.AdministratorService.Persistence;

namespace Covid19.AdministratorService.Persistence.Repository
{
    public class AdminBookingRepository : BaseRepository<AdminBookingAllocation>, IAdminBookingRepository
    {
        public AdminBookingRepository(AdministratorDbContext dbContext) : base(dbContext)
        {
        }



    }
}
