namespace WebApiAuthenticate.Requests;

/// <summary>
/// Базовая сущность запроса
/// </summary>
/// <typeparam name="TId">Идентификатор запроса</typeparam>
public abstract class BaseRequestWithId<TId> 
    where TId : struct
{
    /// <summary>
    /// Идентификатор запроса
    /// </summary>
    public required TId Id { get; init; }
}