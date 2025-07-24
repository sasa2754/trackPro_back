using MediatR;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;

namespace TrackPro.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommandHandler : IRequestHandler<CreateStationCommand, int>
    {
        private readonly IStationRepository _stationRepository;

        public CreateStationCommandHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task<int> Handle(CreateStationCommand request, CancellationToken cancellationToken)
        {

            var station = new Station() { Name = request.Name, Order = request.Order };

            station = await _stationRepository.AddAsync(station);

            return station.Id;
        }
    }
}