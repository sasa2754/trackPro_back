using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand>
    {
        private readonly IPartRepository _partRepository;

        public UpdatePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var partToUpdate = await _partRepository.GetByCodeAsync(request.Code);

            if (partToUpdate == null)
            {
                throw new Exception($"Part with code {request.Code} not found.");
            }

            partToUpdate.UpdateDescription(request.Description);

            await _partRepository.UpdateAsync(partToUpdate);
        }
    }
}