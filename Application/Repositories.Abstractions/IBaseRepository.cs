using Domain.Entities;

namespace Repositories.Abstractions;

public interface IBaseRepository<T, TId>
    where T : IEntity<TId>
{
    /// <summary>
    /// Получить все сущности из репозитория
    /// </summary>
    /// <returns>Сущность репозитория</returns>
    Task<List<T>> GetAllAsync();
    /// <summary>
    /// Получить сущность из репозитория по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор репозитория</param>
    /// <returns>Сущность репозитория</returns>
    Task<T?> GetByIdAsync(TId id);
    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <returns>Сущность репозитория</returns>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// Обновить сущность в репозитории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="newEntity">Сущность репозитория, которой обновляют сущность </param>
    /// <returns>Сущность репозитория</returns>
    Task<T> UpdateAsync(TId id, T newEntity);
}