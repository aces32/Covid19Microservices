using Covid19.LocationService.Core.Dto;
using Covid19.LocationService.Core.Features.Locations.Queries.GetAllAvailableLocations;
using Covid19.LocationService.Core.Features.Locations.Queries.GetLocationsByIds;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Covid19.LocationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// An endpoint that returns all locations available for allocating booking space
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllAvailableLocations")]
        public async Task<ActionResult<List<AvailableLocationDto>>> GetAllAvailableLocations()
        {
            var availableLocationLists = await _mediator.Send(new GetAllAvailableLocationsQuery());
            return Ok(availableLocationLists);
        }

        /// <summary>
        ///  An endpoint that returns a list of locations by their ids
        /// </summary>
        /// <param name="locationIds"></param>
        /// <returns></returns>
        [HttpGet("GetLocationByIds")]
        public async Task<ActionResult<List<AvailableLocationDto>>> GetLocationByIds([FromQuery] int[] locationIds)
        {
            var availableLocations = await _mediator.Send(new GetLocationsByIdsQuery { LocationIds = [.. locationIds] });
            return Ok(availableLocations);
        }


    }
}
