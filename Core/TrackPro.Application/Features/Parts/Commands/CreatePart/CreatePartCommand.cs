using MediatR;

namespace TrackPro.Application.Features.Parts.Commands.CreatePart
{
    public class CreatePartCommand : IRequest<string>
    {
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string Responsible { get; set;}
    }
}