using TrackPro.Domain.Entities;

namespace TrackPro.Application.Contracts.Persistence

{
    public interface IPartRepository : IGenericRepository<Part>
    {
        Task<Part> GetByCodeAsync(string code);
    }
}