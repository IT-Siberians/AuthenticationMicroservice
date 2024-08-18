using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;

/// <summary>
/// Реализация конструктора базового InMemory репозитория
/// </summary>
/// <typeparam name="T">Сущность репозитория</typeparam>
/// <typeparam name="TId">Уникальный идентификатор сущности репозитория</typeparam>
/// <param name="entities">Перечисляемая коллекция сущностей репозитория</param>
public abstract class BaseInMemoryRepository<T, TId>(IEnumerable<T> entities) : IBaseRepository<T, TId> where T : IEntity<TId>
{
    protected List<T> Entities = entities.ToList();

    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    public BaseInMemoryRepository() : this([])
    {

    }

    /// <summary>
    /// Получить все сущности репозитория
    /// </summary>
    /// <returns>Коллекция List сущностей репозитория</returns>
    public Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(Entities.AsEnumerable());
    }

    /// <summary>
    /// Получить сущность из репозитория по индектификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Сущность репозитория</returns>
    public Task<T?> GetByIdAsync(TId id)
    {
        return Task.FromResult(Entities.FirstOrDefault(t => Equals(t.Id, id)));
    }

    /// <summary>
    /// Добавление сущности в репозиторий
    /// </summary>
    /// <param name="entity">Сущность репозитория</param>
    /// <returns>Сущность репозитория</returns>
    public Task<T> AddAsync(T entity)
    {
        Entities.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновление сущности в репозитории по индентификатору
    /// </summary>
    /// <param name="id">идентификатор обновляемой сущности</param>
    /// <param name="newEntity">сущность содержащая информацию, на которую будут обновлять</param>
    /// <returns>Сущность репозитория</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<T> UpdateAsync(TId id, T newEntity)
    {
        var entityToUpdate = await GetByIdAsync(id) ?? throw new ArgumentNullException(nameof(id));
        var index = Entities.IndexOf(entityToUpdate);
        Entities[index] = newEntity;
        return Entities[index];
    }

    /// <summary>
    /// Удалить сущность в репозитории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <returns>true - удалено/false - не удалено(ошибка удаления)</returns>
    public async Task<bool> DeleteAsync(TId id)
    {
        var entityToDelete = await GetByIdAsync(id);
        if (entityToDelete is null)
        {
            return false;
        }

        Entities.Remove(entityToDelete);

        return await GetByIdAsync(id) == null;
    }
}