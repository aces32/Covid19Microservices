using Covid19.IndividualLabsService.Core.Contracts.Persistence;
using Covid19.IndividualLabsService.Domain.Entities;

namespace Covid19.IndividualLabsService.Persistence.Repository
{
    public class IndividualLabRepository : BaseRepository<IndividualLab>, IIndividualLabRepository
    {
        public IndividualLabRepository(IndividualLabsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
