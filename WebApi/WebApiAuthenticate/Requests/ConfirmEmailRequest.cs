namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на подтверждение нового адреса электронной почты пользователя.
/// </summary>
/// <param name="Id">Идентификатор запроса на подтверждение.</param>
/// <param name="NewEmail">Новый адрес электронной почты, который требуется подтвердить.</param>
/// <param name="Code">Код верификации, отправленный на новый адрес электронной почты для подтверждения.</param>
public record ConfirmEmailRequest(
    Guid Id,
    string NewEmail,
    ushort Code) : BaseRequest<Guid>(Id);