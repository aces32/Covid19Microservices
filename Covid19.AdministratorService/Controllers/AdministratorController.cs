using Covid19.AdministratorService.Core.Features.Administrators.Command.AllocateBookingSpace;
using Covid19.AdministratorService.Core.Features.Administrators.Queries.GetAvailableBookingDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.AdministratorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class AdministratorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdministratorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// An endpoint to allocate spaces for tests at specific locations and days.
        /// </summary>
        /// <param name="allocateBookingSpaceCommand"></param>
        /// <returns></returns>
        [HttpPost("/api/Administrator/AllocateBookingSpace", Name = "AllocateBookingSpace")]
        public async Task<ActionResult<AllocateBookingSpaceCommandResponse>> Create(AllocateBookingSpaceCommand allocateBookingSpaceCommand)
        {
            var response = await _mediator.Send(allocateBookingSpaceCommand);
            return Ok(response);
        }

        /// <summary>
        /// An endpoint that shows all Available booking details from query date
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Administrator/GetAvailableBookingDetails", Name = "GetAvailableBookingDetails")]
        public async Task<ActionResult<List<GetAvailableBookingDetailsQueryResponse>>> Get()
        {
            var response = await _mediator.Send(new GetAvailableBookingDetailsQuery());
            return Ok(response);
        }

    }
}
