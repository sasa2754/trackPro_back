using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackPro.Application.Features.Stations.Queries.GetStationList;
using TrackPro.Application.Features.Stations.Commands.CreateStation;
using TrackPro.Application.Features.Stations.Queries.GetStationById;
using TrackPro.Application.Features.Stations.Commands.UpdateStation;

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StationListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StationListDto>> Get(int id)
        {
            var query = new GetStationByIdQuery() { Id = id };
            var station = await _mediator.Send(query);

            if (station == null)
            {
                return NotFound();
            }

            return Ok(station);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateStationCommand command)
        {
            command.Id = id; 

            await _mediator.Send(command);

            return NoContent();
        }
    }
}