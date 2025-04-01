using AutoMapper;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.EventContracts;
using MassTransit;
using MediatR;

namespace Covid19.IndividualService.Core.Features.Command.CancelBookedTest
{
    public class CancelBookedTestCommandHandler : IRequestHandler<CancelBookedTestCommand, CancelBookedTestCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CancelBookedTestCommandHandler> _logger;
        private readonly IIndividualRepository _individualRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBookingAvailabilityRepository _bookingAvailabilityRepository;

        public CancelBookedTestCommandHandler(IMapper mapper, ILogger<CancelBookedTestCommandHandler> logger,
            IIndividualRepository individualRepository, 
            IPublishEndpoint publishEndpoint, IBookingAvailabilityRepository bookingAvailabilityRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _individualRepository = individualRepository;
            _publishEndpoint = publishEndpoint;
            _bookingAvailabilityRepository = bookingAvailabilityRepository;
        }
        public async Task<CancelBookedTestCommandResponse> Handle(CancelBookedTestCommand request, CancellationToken cancellationToken)
        {

            var individualRecord = await _individualRepository.FindWhere(x => x.EmailAddress == request.IndividualEmailAddress
                                                                   && x.MobileNumber == request.IndividualMobileNumber)
                                                                   ?? throw new ArgumentNullException();

            var getAdminProfiling = await _bookingAvailabilityRepository
                                            .FindWhere(x => x.Id == individualRecord.AdminBookingAllocationId)
                                            ?? throw new ArgumentNullException();
            if (!request.IndividualBookingStatus)
                getAdminProfiling.SpaceAllocated -= 1;

            _mapper.Map(request, individualRecord, typeof(CancelBookedTestCommand), typeof(Individual));
            await _individualRepository.UpdateAsync(individualRecord);

            await PublishReducedAllocatedSlotToAdminAsync(getAdminProfiling);

            await PublishCancledEventsToLabsAsync(individualRecord);

            return _mapper.Map<CancelBookedTestCommandResponse>(individualRecord);

        }

        private async Task PublishCancledEventsToLabsAsync(Individual individualRecord)
        {
            await _publishEndpoint.Publish(new CovidTestCancelledEvent
            {
                IndividualId = individualRecord.Id
            });
        }

        private async Task PublishReducedAllocatedSlotToAdminAsync(BookingAvailability getAdminProfiling)
        {
            await _publishEndpoint.Publish(new SpaceAllocatedIncreasedEvent
            {
                AdminBookingAllocationId = getAdminProfiling.Id,
                SpaceAllocated = getAdminProfiling.SpaceAllocated
            });
        }
    }
}
