using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;
public abstract class BaseInMemoryRepository<T, TId> : IBaseRepository<T, TId> where T : IEntity<TId>
{
    protected List<T> Entities;
    public Task<List<T>> GetAllAsync()
    {
        return Task.FromResult(Entities);
    }

    public Task<T?> GetByIdAsync(TId id)
    {
        return Task.FromResult(Entities.FirstOrDefault(t => Equals(t.Id, id)));
    }

    public Task<T> AddAsync(T entity)
    {
        Entities.Add(entity);
        return Task.FromResult(entity);
    }

    public Task AddRangeAsync(ICollection<T> entities)
    {
        Entities.AddRange(entities);
        return Task.CompletedTask;
    }

    public async Task<T> UpdateAsync(TId id, T newEntity)
    {
        var entityToUpdate = await GetByIdAsync(id);
        if (entityToUpdate == null)
            throw new ArgumentNullException(nameof(entityToUpdate));
        var index = Entities.IndexOf(entityToUpdate);
        Entities[index] = newEntity;
        return Entities[index];
    }
}