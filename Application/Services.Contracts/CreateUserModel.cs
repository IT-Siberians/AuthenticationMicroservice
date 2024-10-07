namespace Services.Contracts;

/// <summary>
/// Модель создания пользователя
/// </summary>
public class CreateUserModel
{
    /// <summary>
    /// Имя создаваемого пользователя (никнейм)
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Пароль создаваемого пользователя
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Email создаваемого пользователя
    /// </summary>
    public required string Email { get; init; }
}
