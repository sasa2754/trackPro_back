using MediatR;

namespace TrackPro.Application.Features.Parts.Commands.DeletePart
{
    public class DeletePartCommand : IRequest
    {
        public required string Code { get; set; }
    }
}