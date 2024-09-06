using Domain.Entities;
using Repositories.Abstractions;

namespace Repositories.Implementations.InMemoryRepository;

/// <summary>
/// Реализация конструктора базового InMemory репозитория
/// </summary>
/// <typeparam name="TEntity">Сущность репозитория</typeparam>
/// <typeparam name="TId">Уникальный идентификатор сущности репозитория</typeparam>
/// <param name="entities">Перечисляемая коллекция сущностей репозитория</param>
public abstract class BaseInMemoryRepository<TEntity, TId>(IEnumerable<TEntity> entities) : IBaseRepository<TEntity, TId>
    where TEntity : IEntity<TId>, IDeletableSoftly
    where TId : struct
{
    /// <summary>
    /// Коллекция-хранилище сущностей
    /// </summary>
    protected List<TEntity> Entities = entities.ToList();

    /// <summary>
    /// Конструктор без параметров
    /// </summary>
    protected BaseInMemoryRepository() : this([])
    {

    }

    /// <summary>
    /// Получить все сущности репозитория
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Перечисляемая коллекция сущностей репозитория</returns>
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(Entities.AsEnumerable());
    }

    /// <summary>
    /// Получить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    public Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return cancellationToken.IsCancellationRequested
            ? default
            : Task.FromResult(Entities.FirstOrDefault(t => Equals(t.Id, id)));
    }

    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Сущность репозитория</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавляемая сущность репозитория</returns>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return default;

        Entities.Add(entity);
        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновить сущность по идентификатору
    /// </summary>
    /// <param name="newEntity">Сущность, которой обновляют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновившаяся сущность</returns>
    /// <exception cref="ArgumentNullException">Исключительная ситуация: передача null в параметры</exception>
    public async Task<TEntity> UpdateAsync(TEntity? newEntity, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return default;

        var entityToUpdate = await GetByIdAsync(newEntity.Id, cancellationToken)
                             ?? throw new ArgumentNullException(nameof(newEntity.Id));
        var index = Entities.IndexOf(entityToUpdate);
        Entities[index] = newEntity;
        return Entities[index];
    }

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пользователь помечен как удаленный/ false - пользователь не удален</returns>
    public virtual async Task<bool> DeleteSoftlyAsync(TId id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;

        var entityToDelete = await GetByIdAsync(id, cancellationToken);
        if (entityToDelete is null)
            return false;

        entityToDelete.MarkAsDeletedSoftly();

        return entityToDelete.IsDeleted;
    }
}