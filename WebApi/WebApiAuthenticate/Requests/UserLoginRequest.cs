namespace WebApiAuthenticate.Requests;

/// <summary>
/// Модель запроса для аутентификации пользователя.
/// </summary>
/// <param name="Login">Имя пользователя или электронная почта для входа в систему.</param>
/// <param name="Password">Пароль пользователя для аутентификации.</param>
public record UserLoginRequest(string Login, string Password);