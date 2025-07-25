using MediatR;

namespace TrackPro.Application.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommand : IRequest
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}