using MediatR;

namespace TrackPro.Application.Features.Parts.Queries.GetPartList
{
    public class GetPartListQuery : IRequest<List<PartListDto>>
    {
    }
}