namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос подтверждения Email
/// </summary>
/// <param name="Id">Идентификатор запроса</param>
/// <param name="NewEmail">Новый Email, который подтверждают</param>
/// <param name="CreatedDateTime">Дата создания запроса</param>
public record ConfirmEmailRequest(
    Guid Id,
    string NewEmail,
    DateTime CreatedDateTime) : IBaseRequest<Guid>;