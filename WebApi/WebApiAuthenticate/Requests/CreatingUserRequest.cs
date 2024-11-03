namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на создание пользователя
/// </summary>
/// <param name="Username">Имя создаваемого пользователя (никнейм)</param>
/// <param name="Password">Пароль создаваемого пользователя</param>
/// <param name="Email">Email создаваемого пользователя</param>
public record CreatingUserRequest(
    string Username,
    string Password,
    string Email);