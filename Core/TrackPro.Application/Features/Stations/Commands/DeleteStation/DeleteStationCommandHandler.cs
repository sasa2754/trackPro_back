using MediatR;
using TrackPro.Application.Contracts.Persistence;
using System.Net;
using TrackPro.Application.Exceptions;

namespace TrackPro.Application.Features.Stations.Commands.DeleteStation
{
    public class DeleteStationCommandHandler : IRequestHandler<DeleteStationCommand>
    {
        private readonly IStationRepository _stationRepository;
        private readonly IPartRepository _partRepository;

        public DeleteStationCommandHandler(IStationRepository stationRepository, IPartRepository partRepository)
        {
            _stationRepository = stationRepository;
            _partRepository = partRepository;
        }

        public async Task Handle(DeleteStationCommand request, CancellationToken cancellationToken)
        {
            var stationToDelete = await _stationRepository.GetByIdAsync(request.Id);
            if (stationToDelete == null)
            {
                throw new ApiException(HttpStatusCode.NotFound, $"Station with Id {request.Id} not found.");
            }

            var stationHasParts = await _partRepository.AnyAsync(p => p.CurrentStationId == request.Id);
            if (stationHasParts)
            {
                throw new ApiException(HttpStatusCode.BadRequest, "Cannot delete station because it currently contains parts.");
            }

            await _stationRepository.DeleteAsync(stationToDelete);
        }
    }
}