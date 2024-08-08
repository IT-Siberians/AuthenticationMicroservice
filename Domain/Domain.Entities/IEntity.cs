namespace Domain.Entities;

/// <summary>
/// Маркерный интерфейс сущности
/// </summary>
/// <typeparam name="TId"></typeparam>
public interface IEntity<out TId>
{

    /// <summary>
    /// Идентификатор
    /// </summary>
    public TId Id { get;  }
}