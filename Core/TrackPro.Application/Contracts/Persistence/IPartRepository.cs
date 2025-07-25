using TrackPro.Domain.Entities;
using System.Linq.Expressions;

namespace TrackPro.Application.Contracts.Persistence

{
    public interface IPartRepository : IGenericRepository<Part>
    {
        Task<Part> GetByCodeAsync(string code);
        Task<bool> AnyAsync(Expression<Func<Part, bool>> predicate);
    }
}