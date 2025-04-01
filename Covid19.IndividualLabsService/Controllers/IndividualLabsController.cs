using Covid19.IndividualLabsService.Core.Features.Command.SetTestOutcome;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.IndividualLabsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class IndividualLabsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndividualLabsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// An endpoint where a lab admin can set the outcome of tests.
        /// </summary>
        /// <param name="setTestOutcomeCommand"></param>
        /// <returns></returns>
        [HttpPut("/api/IndividualLabs/SetTestOutcome", Name = "SetTestOutcome")]
        public async Task<ActionResult<SetTestOutcomeCommandResponse>> Update(SetTestOutcomeCommand setTestOutcomeCommand)
        {
            var response = await _mediator.Send(setTestOutcomeCommand);
            return Ok(response);
        }


    }
}
