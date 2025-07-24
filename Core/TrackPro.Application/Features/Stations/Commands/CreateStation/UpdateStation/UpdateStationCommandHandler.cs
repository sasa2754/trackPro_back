using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Stations.Commands.UpdateStation
{
    public class UpdateStationCommandHandler : IRequestHandler<UpdateStationCommand>
    {
        private readonly IStationRepository _stationRepository;

        public UpdateStationCommandHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var stationToUpdate = await _stationRepository.GetByIdAsync(request.Id);

            if (stationToUpdate == null)
            {
                throw new Exception($"Station with Id {request.Id} not found.");
            }

            stationToUpdate.Name = request.Name;
            stationToUpdate.Order = request.Order;

            await _stationRepository.UpdateAsync(stationToUpdate);
        }
    }
}