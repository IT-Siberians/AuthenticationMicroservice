using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;

/// <summary>
/// Репозиторий с CRUD операциями с использованием EF
/// </summary>
/// <typeparam name="TEntity">Сущность репозитория</typeparam>
/// <typeparam name="TId">Идентификатор репозитория</typeparam>
public abstract class BaseEntityFrameworkRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>, IDeletableSoftly
    where TId : struct
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected readonly DbContext DatabaseContext;

    /// <summary>
    /// Коллекция-хранилище сущностей
    /// </summary>
    protected DbSet<TEntity> EntitySet;

    /// <summary>
    /// Репозиторий с CRUD операциями с использованием EF
    /// </summary>
    /// <param name="databaseContext">Контекст базы данных</param>
    protected BaseEntityFrameworkRepository(DbContext databaseContext)
    {
        DatabaseContext = databaseContext;
        EntitySet = DatabaseContext.Set<TEntity>();
    }

    /// <summary>
    /// Получить все сущности репозитория
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Перечисляемая коллекция сущностей репозитория</returns>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await EntitySet.Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);


    /// <summary>
    /// Получить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
        => await EntitySet.FirstOrDefaultAsync(u => u.Id.Equals(id) && !u.IsDeleted, cancellationToken);

    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Сущность репозитория</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавляемая сущность репозитория</returns>
    /// <exception cref="ArgumentNullException">Исключительная ситуация: передача null в параметры</exception>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var addedEntityEntry = EntitySet.Add(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return addedEntityEntry.Entity;
    }

    /// <summary>
    /// Обновить сущность по идентификатору
    /// </summary>
    /// <param name="entity">Сущность, которой обновляют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновившаяся сущность</returns>
    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (!await EntitySet.AnyAsync(u => u.Id.Equals(entity.Id),cancellationToken))
            return null;

        EntitySet.Update(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Удалить пользователя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пользователь помечен как удаленный/ false - пользователь не удален</returns>
    public virtual async Task<bool> DeleteSoftlyAsync(TId id, CancellationToken cancellationToken)
    {
        var entityToDelete = await GetByIdAsync(id, cancellationToken);
        if (entityToDelete is null)
            return false;

        entityToDelete.MarkAsDeletedSoftly();
        EntitySet.Update(entityToDelete);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entityToDelete.IsDeleted;
    }
}