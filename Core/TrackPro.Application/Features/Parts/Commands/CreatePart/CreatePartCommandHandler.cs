using MediatR;
using System.Net;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Application.Exceptions;
using TrackPro.Domain.Entities;

namespace TrackPro.Application.Features.Parts.Commands.CreatePart
{
    public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, string>
    {
        private readonly IPartRepository _partRepository;
        private readonly IMovementRepository _movementRepository;

        public CreatePartCommandHandler(IPartRepository partRepository, IMovementRepository movementRepository)
        {
            _partRepository = partRepository;
            _movementRepository = movementRepository;
        }

        public async Task<string> Handle(CreatePartCommand request, CancellationToken cancellationToken)
        {
            var existingPart = await _partRepository.GetByCodeAsync(request.Code);
            if (existingPart != null)
            {
                throw new ApiException(HttpStatusCode.BadRequest, $"A part with code {request.Code} already exists.");
            }

            var part = new Part(request.Code, request.Description);

            var movement = new Movement(
                part.Code,
                originStationId: null,
                destinationStationId: part.CurrentStationId,
                request.Responsible
            );

            var newPart = await _partRepository.AddAsync(part);
            await _movementRepository.AddAsync(movement);

            return newPart.Code;
        }
    }
}