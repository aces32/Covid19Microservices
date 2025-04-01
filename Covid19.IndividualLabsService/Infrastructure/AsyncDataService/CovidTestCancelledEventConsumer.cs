using Covid19.IndividualLabsService.Core.Contracts.Persistence;
using Covid19.Shared.EventContracts;
using MassTransit;

namespace Covid19.IndividualLabsService.Infrastructure.AsyncDataService
{
    public class CovidTestCancelledEventConsumer : IConsumer<CovidTestCancelledEvent>
    {
        private readonly IIndividualLabRepository _labRepo;
        private readonly ILogger<CovidTestCancelledEventConsumer> _logger;

        public CovidTestCancelledEventConsumer(IIndividualLabRepository labRepo, ILogger<CovidTestCancelledEventConsumer> logger)
        {
            _labRepo = labRepo;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CovidTestCancelledEvent> context)
        {
            var message = context.Message;

            var labRecord = await _labRepo.FindWhere(x => x.IndividualId == message.IndividualId);

            if (labRecord != null)
            {
                labRecord.TestOutCome = "Cancelled";
                await _labRepo.UpdateAsync(labRecord);
            }

            _logger.LogInformation("Lab test cancelled for Individual ID {Id}", message.IndividualId);
        }
    }

}
