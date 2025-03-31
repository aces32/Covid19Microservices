using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.Shared.EventContracts;
using MassTransit;

namespace Covid19.AdministratorService.Infrastructure.AsyncDataService
{
    public class SpaceAllocatedIncreasedEventConsumer : IConsumer<SpaceAllocatedIncreasedEvent>
    {
        private readonly IAdminBookingRepository _repository;

        public SpaceAllocatedIncreasedEventConsumer(IAdminBookingRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<SpaceAllocatedIncreasedEvent> context)
        {
            var message = context.Message;

            var allocation = await _repository.GetByIdAsync(message.AdminBookingAllocationId);
            if (allocation != null)
            {
                allocation.SpaceAllocated = message.SpaceAllocated;
                await _repository.UpdateAsync(allocation);
            }
        }
    }

}
