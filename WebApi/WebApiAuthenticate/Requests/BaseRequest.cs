namespace WebApiAuthenticate.Requests;

/// <summary>
/// Базовая сущность запроса
/// </summary>
/// <typeparam name="TId">Идентификатор запроса</typeparam>
/// <param name="Id">Идентификатор запроса</param>
public abstract record BaseRequest<TId>(TId Id)
    where TId : struct;