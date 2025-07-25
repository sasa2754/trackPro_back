using MediatR;
using TrackPro.Application.Contracts.Persistence;

namespace TrackPro.Application.Features.Parts.Queries.GetPartList
{
    public class GetPartListQueryHandler : IRequestHandler<GetPartListQuery, List<PartListDto>>
    {
        private readonly IPartRepository _partRepository;

        public GetPartListQueryHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<List<PartListDto>> Handle(GetPartListQuery request, CancellationToken cancellationToken)
        {
            var allParts = await _partRepository.GetAllAsync();

            var partDtoList = allParts.Select(part => new PartListDto
            {
                Code = part.Code,
                Description = part.Description,
                Status = part.Status
            }).ToList();

            return partDtoList;
        }
    }
}