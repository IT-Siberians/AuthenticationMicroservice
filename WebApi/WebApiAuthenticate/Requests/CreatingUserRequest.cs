namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на создание пользователя
/// </summary>
public class CreatingUserRequest
{
    /// <summary>
    /// Имя пользователя(никнейм)
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; init; }
}