using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.IndividualService.Persistence;
using Covid19.IndividualService.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace COVID_19PCR.TestManagement.Persistence.Repository
{
    public class IndividualRepository : BaseRepository<Individual>, IIndividualRepository
    {
        public IndividualRepository(IndividualDbContext dbContext) : base(dbContext)
        {
        }

    }
}
