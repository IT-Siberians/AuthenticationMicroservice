using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstractions;

namespace Repositories.Implementations.EntityFrameworkRepositories;
public abstract class BaseEntityFrameworkRepository<T, TId> : IBaseRepository<T, TId> where T : class, IEntity<TId>
{
    protected BaseEntityFrameworkRepository(DbContext context)
    {
        Context = context;
        EntitySet = Context.Set<T>();
    }

    protected readonly DbContext Context;
    protected DbSet<T> EntitySet;

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return EntitySet;
    }

    public virtual async Task<T?> GetByIdAsync(TId id)
    {
        return await EntitySet.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        var addedEntity = EntitySet.Add(entity);
        await Context.SaveChangesAsync();
        return addedEntity.Entity;
    }

    public virtual async Task<T> UpdateAsync(TId id, T newEntity)
    {
        var entityToUpdate = await GetByIdAsync(id);
        if (entityToUpdate == null)
            throw new ArgumentNullException(nameof(id));
        entityToUpdate = newEntity;
        EntitySet.Update(entityToUpdate);
        await Context.SaveChangesAsync();
        return await GetByIdAsync(id);
    }

    public virtual async Task<bool> DeleteAsync(TId id)
    {
        var entityToDelete = await GetByIdAsync(id);
        if (entityToDelete == null)
        {
            return false;
        }
        EntitySet.Remove(entityToDelete);
        await Context.SaveChangesAsync();
        return true;
    }
}