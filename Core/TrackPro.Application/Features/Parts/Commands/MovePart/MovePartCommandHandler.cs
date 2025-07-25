using MediatR;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;

namespace TrackPro.Application.Features.Parts.Commands.MovePart
{
    public class MovePartCommandHandler : IRequestHandler<MovePartCommand>
    {
        private readonly IPartRepository _partRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IMovementRepository _movementRepository;

        public MovePartCommandHandler(IPartRepository partRepository, IStationRepository stationRepository, IMovementRepository movementRepository)
        {
            _partRepository = partRepository;
            _stationRepository = stationRepository;
            _movementRepository = movementRepository;
        }

        public async Task Handle(MovePartCommand request, CancellationToken cancellationToken)
        {
            var partToMove = await _partRepository.GetByCodeAsync(request.PartCode);
            if (partToMove == null)
            {
                throw new Exception($"Part with code {request.PartCode} not found.");
            }

            if (partToMove.Status == "Finalizada")
            {
                throw new Exception("Cannot move a part that is already finished.");
            }

            var currentStation = await _stationRepository.GetByIdAsync(partToMove.CurrentStationId);
            if (currentStation == null)
            {
                throw new Exception($"Inconsistent data: Current station with Id {partToMove.CurrentStationId} not found for part {partToMove.Code}.");
            }

            var nextStationOrder = currentStation.Order + 1;
            var nextStation = await _stationRepository.GetByOrderAsync(nextStationOrder);

            if (nextStation != null)
            {
                partToMove.MoveToNextStation(nextStation);

                var movement = new Movement(partToMove.Code, currentStation.Id, nextStation.Id, request.Responsible);
                await _movementRepository.AddAsync(movement);
            }
            else
            {
                partToMove.FinishProcess();

                var movement = new Movement(partToMove.Code, currentStation.Id, 0, request.Responsible);
                await _movementRepository.AddAsync(movement);
            }

            await _partRepository.UpdateAsync(partToMove);
        }
    }
}