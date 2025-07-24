using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Stations.Commands.DeleteStation
{
    public class DeleteStationCommandHandler : IRequestHandler<DeleteStationCommand>
    {
        private readonly IStationRepository _stationRepository;

        public DeleteStationCommandHandler(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public async Task Handle(DeleteStationCommand request, CancellationToken cancellationToken)
        {
            var stationToDelete = await _stationRepository.GetByIdAsync(request.Id);

            if (stationToDelete == null)
            {
                throw new Exception($"Station with Id {request.Id} not found.");
            }

            await _stationRepository.DeleteAsync(stationToDelete);
        }
    }
}