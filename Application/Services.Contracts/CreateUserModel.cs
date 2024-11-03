namespace Services.Contracts;

/// <summary>
/// Модель создания пользователя
/// </summary>
/// <param name="Username">Имя создаваемого пользователя (никнейм)</param>
/// <param name="Password">Пароль создаваемого пользователя</param>
/// <param name="Email">Email создаваемого пользователя</param>
public record CreateUserModel(
    string Username,
    string Password,
    string Email);