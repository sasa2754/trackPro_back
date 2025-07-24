using MediatR;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Features.Stations.Queries.GetStationList;

namespace TrackPro.Application.Features.Stations.Queries.GetStationById
{
    public class GetStationByIdQueryHandler : IRequestHandler<GetStationByIdQuery, StationListDto>
    {
        private readonly IStationRepository _stationRepository;

        public GetStationByIdQueryHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<StationListDto> Handle(GetStationByIdQuery request, CancellationToken cancellationToken)
        {
            var station = await _stationRepository.GetByIdAsync(request.Id);

            if (station == null)
            {
                return null;
            }

            return new StationListDto
            {
                Id = station.Id,
                Name = station.Name,
                Order = station.Order
            };
        }
    }
}