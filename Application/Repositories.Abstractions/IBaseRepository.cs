using Domain.Entities;

namespace Repositories.Abstractions;

public interface IBaseRepository<T, in TId>
    where T : IEntity<TId> where TId : struct
{
    /// <summary>
    /// Получить все сущности из репозитория
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Перечисляемая коллекция сущностей репозитория</returns>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность из репозитория по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор репозитория</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность в репозитории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="newEntity">Сущность репозитория, которой обновляют сущность </param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<T> UpdateAsync(TId id, T newEntity, CancellationToken cancellationToken);
}