using Microsoft.EntityFrameworkCore;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;
using TrackPro.Infrastructure.Persistence.DbContexts;

namespace TrackPro.Infrastructure.Persistence.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly TrackProDbContext _context;

        public MovementRepository(TrackProDbContext context)
        {
            _context = context;
        }
        
        public async Task<Movement> AddAsync(Movement entity)
        {
            await _context.Movements.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IReadOnlyList<Movement>> GetByPartCodeAsync(string partCode)
        {
            return await _context.Movements
                .Where(m => m.CodePart == partCode)
                .AsNoTracking()
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Movement>> GetAllAsync()
        {
            return await _context.Movements.AsNoTracking().ToListAsync();
        }

        public Task<Movement> GetByIdAsync(int id) => throw new NotImplementedException("A entidade Movement é identificada por um Guid.");
        
        public async Task UpdateAsync(Movement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Movement entity)
        {
            _context.Movements.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}