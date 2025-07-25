using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Parts.Queries.GetPartByCode
{
    public class GetPartByCodeQueryHandler : IRequestHandler<GetPartByCodeQuery, PartDetailDto>
    {
        private readonly IPartRepository _partRepository;
        private readonly IStationRepository _stationRepository;

        public GetPartByCodeQueryHandler(IPartRepository partRepository, IStationRepository stationRepository)
        {
            _partRepository = partRepository;
            _stationRepository = stationRepository;
        }

        public async Task<PartDetailDto> Handle(GetPartByCodeQuery request, CancellationToken cancellationToken)
        {
            var part = await _partRepository.GetByCodeAsync(request.Code);

            if (part == null)
            {
                return null;
            }

            var station = await _stationRepository.GetByIdAsync(part.CurrentStationId);

            return new PartDetailDto
            {
                Code = part.Code,
                Description = part.Description,
                Status = part.Status,
                CurrentStationName = station?.Name ?? "N/A"
            };
        }
    }
}