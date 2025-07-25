using MediatR;

namespace TrackPro.Application.Features.Movements.Queries.GetMovementHistoryByPartCode
{
    public class GetMovementHistoryByPartCodeQuery : IRequest<List<MovementHistoryDto>>
    {
        public required string PartCode { get; set; }
    }
}