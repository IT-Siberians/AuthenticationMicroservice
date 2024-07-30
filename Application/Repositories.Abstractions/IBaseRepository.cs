using Domain.Entities;

namespace Repositories.Abstractions;

public interface IBaseRepository<T, TId>
    where T : IEntity<TId>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(TId id);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(ICollection<T>  entities);
    Task<T> UpdateAsync(TId id, T newEntity);
}