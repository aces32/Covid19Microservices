using Covid19.AdministratorService.Domain.Entities;

namespace Covid19.AdministratorService.Core.Contracts.Persistence
{
    public interface IAdminBookingRepository : IAsyncRepository<AdminBookingAllocation>
    {


    }
}