using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;
public abstract class BaseInMemoryRepository<T, TId>(IEnumerable<T> entities) : IBaseRepository<T, TId> where T : IEntity<TId>
{
    protected List<T> Entities = entities.ToList();

    public BaseInMemoryRepository() : this([])
    {

    }
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
        var entityToUpdate = await GetByIdAsync(id) ?? throw new ArgumentNullException(nameof(id));
        var index = Entities.IndexOf(entityToUpdate);
        Entities[index] = newEntity;
        return Entities[index];
    }
}