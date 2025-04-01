using AutoMapper;
using Covid19.IndividualLabsService.Core.Contracts.Persistence;
using Covid19.IndividualLabsService.Domain.Entities;
using Covid19.Shared.EventContracts;
using MassTransit;

namespace Covid19.IndividualLabsService.Infrastructure.AsyncDataService
{
    public class CovidTestBookedEventConsumer : IConsumer<CovidTestBookedEvent>
    {
        private readonly IIndividualLabRepository _labRepository;
        private readonly IMapper _mapper;

        public CovidTestBookedEventConsumer(IIndividualLabRepository labRepository, IMapper mapper)
        {
            _labRepository = labRepository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CovidTestBookedEvent> context)
        {
            var evt = context.Message;

            var labRecord = new IndividualLab
            {
                IndividualId = evt.IndividualId,
                TestType = (TestType)evt.TestType,
                TestOutCome = "pending",
                BookingDate = evt.BookingDate,
                TestCompleted = false
            };

            await _labRepository.AddAsync(labRecord);
        }
    }

}
