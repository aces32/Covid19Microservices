using AutoMapper;
using Covid19.IndividualService.Core.Contracts.Persistence;
using Covid19.IndividualService.Domain.Entities;
using Covid19.Shared.EventContracts;
using Covid19.Shared.Exceptions;
using MassTransit;
using MediatR;

namespace Covid19.IndividualService.Core.Features.Command.BookCovidTest
{
    public class BookCovidTestCommandHandler : IRequestHandler<BookCovidTestCommand, BookCovidTestCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BookCovidTestCommandHandler> _logger;
        private readonly IIndividualRepository _individualRepository;
        private readonly IBookingAvailabilityRepository _bookingAvailabilityRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public BookCovidTestCommandHandler(IMapper mapper, ILogger<BookCovidTestCommandHandler> logger,
            IIndividualRepository individualRepository, 
            IBookingAvailabilityRepository bookingAvailabilityRepository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _logger = logger;
            _individualRepository = individualRepository;
            _bookingAvailabilityRepository = bookingAvailabilityRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<BookCovidTestCommandResponse> Handle(BookCovidTestCommand request, CancellationToken cancellationToken)
        {
            var start = request.BookingDate;
            var end = start.AddDays(1);
            var getAdminProfiling = await _bookingAvailabilityRepository.FindWhere(x =>
                                                                x.LocationId == request.LocationId &&
                                                                x.BookingDate >= start &&
                                                                x.BookingDate < end);

            if (getAdminProfiling == null)
            {
                throw new NotFoundException(nameof(getAdminProfiling), request.LocationId);

            }
            if (getAdminProfiling.SpaceAllocated >= getAdminProfiling.Capacity)
            {
                throw new BadRequestException("Capacity at the specified location and date is full");
            }

            var individualData = _mapper.Map<Individual>(request);
            individualData.AdminBookingAllocationId = getAdminProfiling.AdminBookingAllocationId;

            var insertIndividualDetails = await _individualRepository.AddAsync(individualData);

            //Increase total allowed capacity and update the allocation table
            getAdminProfiling.SpaceAllocated += 1;
            await _bookingAvailabilityRepository.UpdateAsync(getAdminProfiling);

            await PublishAddedAllocatedSpaceToIndividualServiceAsync(getAdminProfiling);

            await PublishBookedCovidTestToLabsAsync(request, insertIndividualDetails);

            return _mapper.Map<BookCovidTestCommandResponse>(insertIndividualDetails);

        }

        private async Task PublishBookedCovidTestToLabsAsync(BookCovidTestCommand request, Individual insertIndividualDetails)
        {
            await _publishEndpoint.Publish(new CovidTestBookedEvent
            {
                IndividualId = insertIndividualDetails.Id,
                EmailAddress = insertIndividualDetails.EmailAddress,
                BookingDate = request.BookingDate,
                LocationId = request.LocationId,
                TestType = (TestTypeEvent)request.IndividualLab.TestType
            });
        }

        private async Task PublishAddedAllocatedSpaceToIndividualServiceAsync(BookingAvailability getAdminProfiling)
        {
            await _publishEndpoint.Publish(new SpaceAllocatedIncreasedEvent
            {
                AdminBookingAllocationId = getAdminProfiling.Id,
                SpaceAllocated = getAdminProfiling.SpaceAllocated
            });
        }
    }
}
