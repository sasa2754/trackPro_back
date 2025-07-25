using Microsoft.EntityFrameworkCore;
using TrackPro.Application.Contracts.Persistence;
using TrackPro.Domain.Entities;
using TrackPro.Infrastructure.Persistence.DbContexts;
using System.Linq.Expressions;

namespace TrackPro.Infrastructure.Persistence.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly TrackProDbContext _context;

        public PartRepository(TrackProDbContext context)
        {
            _context = context;
        }

        public async Task<Part> AddAsync(Part entity)
        {
            await _context.Parts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Part> GetByCodeAsync(string code)
        {
            return await _context.Parts
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IReadOnlyList<Part>> GetAllAsync()
        {
            return await _context.Parts.AsNoTracking().ToListAsync();
        }
        
        public async Task UpdateAsync(Part entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Part entity)
        {
            _context.Parts.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Part, bool>> predicate)
        {
            return await _context.Parts.AnyAsync(predicate);
        }

        public Task<Part> GetByIdAsync(int id) => throw new NotImplementedException("A entidade Part é identificada por um Code (string), não por um Id (int). Use GetByCodeAsync.");
    }
}