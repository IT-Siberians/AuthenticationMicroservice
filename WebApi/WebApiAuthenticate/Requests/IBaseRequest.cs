namespace WebApiAuthenticate.Requests;

/// <summary>
/// Маркерный интерфейс для запроса с идентификатором
/// </summary>
/// <typeparam name="TId">Идентификатор запроса</typeparam>
public interface IBaseRequest<out TId>
    where TId : struct
{
    TId Id { get; }
}