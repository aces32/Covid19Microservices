using AutoMapper;
using Covid19.IndividualLabsService.Core.Contracts.Persistence;
using Covid19.IndividualLabsService.Domain.Entities;
using Covid19.Shared.Exceptions;
using MediatR;

namespace Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome
{
    public class SetTestOutcomeCommandHandler : IRequestHandler<SetTestOutcomeCommand, SetTestOutcomeCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SetTestOutcomeCommandHandler> _logger;
        private readonly IIndividualLabRepository _individualLabRepository;

        public SetTestOutcomeCommandHandler(IMapper mapper, ILogger<SetTestOutcomeCommandHandler> logger,
            IIndividualLabRepository individualLabRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _individualLabRepository = individualLabRepository;
        }
        public async Task<SetTestOutcomeCommandResponse> Handle(SetTestOutcomeCommand request, CancellationToken cancellationToken)
        {
            var getIndividualLabsRecords = await _individualLabRepository.FindWhere(x => x.IndividualId == request.IndividualId);
            if (getIndividualLabsRecords == null)
            {
                throw new NotFoundException(nameof(getIndividualLabsRecords), request.IndividualId);
            }

            _mapper.Map<SetTestOutcomeCommand, IndividualLab>(request, getIndividualLabsRecords);

            await _individualLabRepository.UpdateAsync(getIndividualLabsRecords);
            return _mapper.Map<SetTestOutcomeCommandResponse>(getIndividualLabsRecords);

        }
    }
}
