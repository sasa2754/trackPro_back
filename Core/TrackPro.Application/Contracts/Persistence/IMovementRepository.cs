using TrackPro.Domain.Entities;

namespace TrackPro.Application.Contracts.Persistence
{
    public interface IMovementRepository : IGenericRepository<Movement>
    {
        Task<IReadOnlyList<Movement>> GetByPartCodeAsync(string partCode);
    }
}