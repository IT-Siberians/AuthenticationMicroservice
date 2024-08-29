using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;

/// <summary>
/// Репозиторий с CRUD операциями с использованием EF
/// </summary>
/// <typeparam name="TEntity">Сущность репозитория</typeparam>
/// <typeparam name="TId">Идентификатор репозитория</typeparam>
public abstract class BaseEntityFrameworkRepository<TEntity, TId> : IBaseRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected readonly DbContext DatabaseContext;

    /// <summary>
    /// Коллекция-хранилище сущностей
    /// </summary>
    protected DbSet<TEntity?> EntitySet;

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
    public virtual async Task<IEnumerable<TEntity?>> GetAllAsync(CancellationToken cancellationToken)
        => await EntitySet.Where(u=>u.IsDeleted == false).ToListAsync(cancellationToken);
    

    /// <summary>
    /// Получить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        var users = await GetAllAsync(cancellationToken);
        return users.FirstOrDefault(u => u.Id.Equals(id));
    }

    /// <summary>
    /// Добавить сущность в репозиторий
    /// </summary>
    /// <param name="entity">Сущность репозитория</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавляемая сущность репозитория</returns>
    /// <exception cref="ArgumentNullException">Исключительная ситуация: передача null в параметры</exception>
    public virtual async Task<TEntity?> AddAsync(TEntity? entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var addedEntity = EntitySet.Add(entity);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity;
    }

    /// <summary>
    /// Обновить сущность по идентификатору
    /// </summary>
    /// <param name="newEntity">Сущность, которой обновляют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновившаяся сущность</returns>
    public virtual async Task<TEntity?> UpdateAsync(TEntity? newEntity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await GetByIdAsync(newEntity.Id, cancellationToken);
        if (entityToUpdate == null)
            return null;

        entityToUpdate = newEntity;
        EntitySet.Update(entityToUpdate);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}