namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену пароля
/// </summary>
/// <param name="OldPassword">Проверяемый пароль</param>
/// <param name="NewPassword">Новый пароль, на который происходит смена</param>
public record ChangePasswordRequest(
    string OldPassword,
    string NewPassword);