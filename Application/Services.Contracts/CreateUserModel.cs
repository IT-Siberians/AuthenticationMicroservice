namespace Services.Contracts;

/// <summary>
/// Модель создания пользователя
/// </summary>
/// <param name="Username">Никнейм создаваемого пользователя</param>
/// <param name="Firstname">Имя создаваемого пользователя</param>
/// <param name="Lastname">Фамилия создаваемого пользователя</param>
/// <param name="Password">Пароль создаваемого пользователя</param>
/// <param name="Email">Email создаваемого пользователя</param>
public record CreateUserModel(
    string Username,
    string Firstname,
    string Lastname,
    string Password,
    string Email);