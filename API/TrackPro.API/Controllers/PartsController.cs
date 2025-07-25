using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackPro.Application.Features.Parts.Commands.CreatePart;
using TrackPro.Application.Features.Parts.Queries.GetPartList;

namespace TrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Post([FromBody] CreatePartCommand command)
        {
            var newPartCode = await _mediator.Send(command);

            return StatusCode(201, newPartCode);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PartListDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PartListDto>>> Get()
        {
            var query = new GetPartListQuery();
            var parts = await _mediator.Send(query);
            return Ok(parts);
        }
    }
}