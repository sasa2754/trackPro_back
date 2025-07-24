using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Stations.Queries.GetStationList
{
    public class GetStationListQueryHandler : IRequestHandler<GetStationListQuery, List<StationListDto>>
    {
        private readonly IStationRepository _stationRepository;

        public GetStationListQueryHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<List<StationListDto>> Handle(GetStationListQuery request, CancellationToken cancellationToken)
        {
            var stationsFromDb = await _stationRepository.GetAllAsync();

            var stationDtoList = stationsFromDb.Select(station => new StationListDto
            {
                Id = station.Id,
                Name = station.Name,
                Order = station.Order
            }).ToList();

            return stationDtoList;
        }
    }
}