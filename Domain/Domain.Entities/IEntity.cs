namespace Domain.Entities;

/// <summary>
/// Маркерный интерфейс сущности
/// </summary>
/// <typeparam name="TId">Идентификатор сущности</typeparam>
public interface IEntity<out TId> where TId : struct
{

    /// <summary>
    /// Идентификатор
    /// </summary>
    public TId Id { get;  }

    /// <summary>
    /// Метка удаления
    /// </summary>
    public bool IsDeleted { get; }
}