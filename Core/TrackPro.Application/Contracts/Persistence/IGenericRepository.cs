namespace TrackPro.Application.Contracts.Persistence

// Interface genérica para evitar repetição de código, ela está definindo os CRUDS em comum de todas as entidades
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}