namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену пароля
/// </summary>
public class ChangePasswordRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Проверяемый пароль
    /// </summary>
    public required string OldPassword { get; init; }

    /// <summary>
    /// Новый пароль, на который происходит смена
    /// </summary>
    public required string NewPassword { get; init; }
}