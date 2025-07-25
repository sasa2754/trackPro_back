using MediatR;

namespace TrackPro.Application.Features.Parts.Commands.MovePart
{
    public class MovePartCommand : IRequest
    {
        public required string PartCode { get; set; }

        public required string Responsible { get; set; }
    }
}