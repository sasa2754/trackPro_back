using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Movements.Queries.GetMovementHistoryByPartCode
{
    public class GetMovementHistoryByPartCodeQueryHandler : IRequestHandler<GetMovementHistoryByPartCodeQuery, List<MovementHistoryDto>>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IStationRepository _stationRepository;

        public GetMovementHistoryByPartCodeQueryHandler(IMovementRepository movementRepository, IStationRepository stationRepository)
        {
            _movementRepository = movementRepository;
            _stationRepository = stationRepository;
        }

        public async Task<List<MovementHistoryDto>> Handle(GetMovementHistoryByPartCodeQuery request, CancellationToken cancellationToken)
        {
            var stations = await _stationRepository.GetAllAsync();
            var stationLookup = stations.ToDictionary(s => s.Id, s => s.Name);

            stationLookup[0] = "Finalizada";

            var movements = await _movementRepository.GetByPartCodeAsync(request.PartCode);

            var historyDto = movements.Select(mov => new MovementHistoryDto
            {
                Date = mov.Date,
                OriginStationName = mov.OriginStationId.HasValue ? stationLookup.GetValueOrDefault(mov.OriginStationId.Value, "N/A") : "Entrada no Sistema",
                DestinationStationName = stationLookup.GetValueOrDefault(mov.DestinationStationId, "N/A"),
                Responsible = mov.Responsible
            }).ToList();

            return historyDto;
        }
    }
}