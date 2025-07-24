using MediatR;

namespace TrackPro.Application.Features.Stations.Queries.GetStationList
{
    public class GetStationListQuery : IRequest<List<StationListDto>>
    {
    }
}