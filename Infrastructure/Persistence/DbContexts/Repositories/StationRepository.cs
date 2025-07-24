using Microsoft.EntityFrameworkCore;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;
using TrackPro.Infrastructure.Persistence.DbContexts;

namespace TrackPro.Infrastructure.Persistence.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly TrackProDbContext _context;

        public StationRepository(TrackProDbContext context)
        {
            _context = context;
        }

        public async Task<Station> AddAsync(Station entity)
        {
            await _context.Stations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Station entity)
        {
            _context.Stations.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Station>> GetAllAsync()
        {
            return await _context.Stations.AsNoTracking().ToListAsync();
        }

        public async Task<Station> GetByIdAsync(int id)
        {
            return await _context.Stations.FindAsync(id);
        }

        public async Task<Station> GetByOrderAsync(int order)
        {
            return await _context.Stations
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Order == order);
        }

        public async Task UpdateAsync(Station entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}