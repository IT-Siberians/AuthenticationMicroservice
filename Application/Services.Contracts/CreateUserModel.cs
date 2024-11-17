namespace Services.Contracts;

/// <summary>
/// Модель создания пользователя
/// </summary>
/// <param name="Username">Никнейм создаваемого пользователя</param>
/// <param name="FirstName">Имя создаваемого пользователя</param>
/// <param name="LastName">Фамилия создаваемого пользователя</param>
/// <param name="Password">Пароль создаваемого пользователя</param>
/// <param name="Email">Email создаваемого пользователя</param>
public record CreateUserModel(
    string Username,
    string FirstName,
    string LastName,
    string Password,
    string Email);