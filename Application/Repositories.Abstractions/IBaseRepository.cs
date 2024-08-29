using Domain.Entities;

namespace Repositories.Abstractions;

/// <summary>
/// Интерфейс базового репозитория с CRUD операциями
/// </summary>
/// <typeparam name="TEntity">Обобщение сущности репозитория</typeparam>
/// <typeparam name="TId">Идентификатор репозитория</typeparam>
public interface IBaseRepository<TEntity, in TId>
    where TEntity : IEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Получить все сущности из репозитория
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Перечисляемая коллекция сущностей репозитория</returns>
    Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность из репозитория по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор репозитория</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<TEntity?> AddAsync(TEntity? entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность в репозитории по идентификатору
    /// </summary>
    /// <param name="newEntity">Сущность репозитория, которой обновляют сущность </param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    Task<TEntity?> UpdateAsync(TEntity? newEntity, CancellationToken cancellationToken);
}