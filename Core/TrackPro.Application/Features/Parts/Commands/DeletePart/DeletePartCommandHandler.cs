using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Parts.Commands.DeletePart
{
    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand>
    {
        private readonly IPartRepository _partRepository;

        public DeletePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            var partToDelete = await _partRepository.GetByCodeAsync(request.Code);

            if (partToDelete == null)
            {
                throw new Exception($"Part with code {request.Code} not found.");
            }

            await _partRepository.DeleteAsync(partToDelete);
        }
    }
}