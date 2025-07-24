using TrackPro.Domain.Entities;

namespace TrackPro.Application.Contracts.Persistence
{
    public interface IStationRepository : IGenericRepository<Station>
    {
        Task<Station> GetByOrderAsync(int order);
    }
}