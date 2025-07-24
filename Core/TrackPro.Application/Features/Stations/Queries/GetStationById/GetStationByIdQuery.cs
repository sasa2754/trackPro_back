using MediatR;
using TrackPro.Application.Features.Stations.Queries.GetStationList;

namespace TrackPro.Application.Features.Stations.Queries.GetStationById
{
    public class GetStationByIdQuery : IRequest<StationListDto>
    {
        public int Id { get; set; }
    }
}