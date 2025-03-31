using AutoMapper;
using Covid19.AdministratorService.Core.Contracts.Persistence;
using Covid19.AdministratorService.Domain.Entities;
using Covid19.Shared.EventContracts;
using MassTransit;
using MediatR;

namespace Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace
{
    public class AllocateBookingSpaceCommandHandler : IRequestHandler<AllocateBookingSpaceCommand, AllocateBookingSpaceCommandResponse>
    {
        private readonly IAdminBookingRepository _adminBookingRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AllocateBookingSpaceCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public AllocateBookingSpaceCommandHandler(IAdminBookingRepository adminBookingRepository,
            IMapper mapper, ILogger<AllocateBookingSpaceCommandHandler> logger,
            IPublishEndpoint publishEndpoint)
        {
            _adminBookingRepository = adminBookingRepository;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<AllocateBookingSpaceCommandResponse> Handle(AllocateBookingSpaceCommand request, CancellationToken cancellationToken)
        {
            var allocateBookingSpace = await _adminBookingRepository.AddAsync(_mapper.Map<AdminBookingAllocation>(request), true);

            // Publish event to message broker
            var bookingSlotCreatedEvent = _mapper.Map<BookingSlotCreatedEvent>(allocateBookingSpace);

            await _publishEndpoint.Publish(bookingSlotCreatedEvent, cancellationToken);
            return _mapper.Map<AllocateBookingSpaceCommandResponse>(allocateBookingSpace);

        }
    }
}
