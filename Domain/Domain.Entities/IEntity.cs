namespace Domain.Entities;

/// <summary>
/// Маркерный интерфейс сущности
/// </summary>
/// <typeparam name="TId">Идентификатор сущности</typeparam>
public interface IEntity<out TId>
{

    /// <summary>
    /// Идентификатор
    /// </summary>
    public TId Id { get;  }
}