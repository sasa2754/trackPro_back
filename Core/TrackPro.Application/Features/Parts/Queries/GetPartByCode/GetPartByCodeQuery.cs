using MediatR;

namespace TrackPro.Application.Features.Parts.Queries.GetPartByCode
{
    public class GetPartByCodeQuery : IRequest<PartDetailDto>
    {
        public required string Code { get; set; }
    }
}