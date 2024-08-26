using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;

/// <summary>
/// Репозиторий с CRUD операциями с использованием EF
/// </summary>
/// <typeparam name="T">Сущность репозитория</typeparam>
/// <typeparam name="TId">Идентификатор репозитория</typeparam>
public abstract class BaseEntityFrameworkRepository<T, TId> : IBaseRepository<T, TId> where T : class, IEntity<TId> where TId : struct
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected readonly DbContext Context;

    /// <summary>
    /// Коллекция-хранилище сущностей
    /// </summary>
    protected DbSet<T> EntitySet;

    /// <summary>
    /// Репозиторий с CRUD операциями с использованием EF
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    protected BaseEntityFrameworkRepository(DbContext context)
    {
        Context = context;
        EntitySet = Context.Set<T>();
    }

    /// <summary>
    /// Получить все сущности репозитория
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Перечисляемая коллекция сущностей репозитория</returns>
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        => await EntitySet.Where(u=>u.IsDeleted == false).ToListAsync(cancellationToken);
    

    /// <summary>
    /// Получить сущность по её идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность репозитория</returns>
    public virtual async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
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
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        var addedEntity = EntitySet.Add(entity);
        await Context.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity;
    }

    /// <summary>
    /// Обновить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор обновляемой сущности</param>
    /// <param name="newEntity">Сущность, которой обновляют</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновившаяся сущность</returns>
    /// <exception cref="ArgumentNullException">Исключительная ситуация: передача null в параметры</exception>
    public virtual async Task<T> UpdateAsync(TId id, T newEntity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await GetByIdAsync(id, cancellationToken);
        if (entityToUpdate == null)
            throw new ArgumentNullException(nameof(id));

        entityToUpdate = newEntity;
        EntitySet.Update(entityToUpdate);
        await Context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}