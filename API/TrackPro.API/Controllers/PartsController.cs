using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrackPro.Application.Features.Parts.Commands.CreatePart;
using TrackPro.Application.Features.Parts.Queries.GetPartList;
using TrackPro.Application.Features.Parts.Queries.GetPartByCode;
using TrackPro.Application.Features.Parts.Commands.MovePart;
using TrackPro.Application.Features.Parts.Commands.UpdatePart;

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

        public class MovePartRequest
        {
            public required string Responsible { get; set; }
        }

        public class UpdatePartRequest
        {
            public required string Description { get; set; }
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

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(PartDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PartDetailDto>> Get(string code)
        {
            var query = new GetPartByCodeQuery() { Code = code };
            var part = await _mediator.Send(query);

            if (part == null)
            {
                return NotFound();
            }

            return Ok(part);
        }

        [HttpPost("{code}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Move(string code, [FromBody] MovePartRequest request)
        {
            var command = new MovePartCommand()
            {
                PartCode = code,
                Responsible = request.Responsible
            };

            try
            {
                await _mediator.Send(command);
                return Ok(new { message = $"Part {code} moved successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(string code, [FromBody] UpdatePartRequest request)
        {
            var command = new UpdatePartCommand
            {
                Code = code,
                Description = request.Description
            };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}