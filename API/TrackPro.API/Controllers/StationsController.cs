using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackPro.Application.Features.Stations.Commands.CreateStation;
using TrackPro.Application.Features.Stations.Queries.GetStationList;

namespace TrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<StationListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StationListDto>>> Get()
        {
            var query = new GetStationListQuery();

            var stations = await _mediator.Send(query);

            return Ok(stations);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<int>> Post([FromBody] CreateStationCommand command)
        {
            var newStationId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = newStationId }, newStationId);
        }
    }
}