namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на создание пользователя
/// </summary>
/// <param name="Username">Никнейм создаваемого пользователя</param>
/// <param name="FirstName">Имя создаваемого пользователя</param>
/// <param name="Lastname">Фамилия создаваемого пользователя</param>
/// <param name="Password">Пароль создаваемого пользователя</param>
/// <param name="Email">Email создаваемого пользователя</param>
public record CreatingUserRequest(
    string Username,
    string FirstName,
    string Lastname,
    string Password,
    string Email);