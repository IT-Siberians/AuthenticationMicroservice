using Domain.Entities.Domain.Enums;

namespace WebApiAuthenticate.Responses;

/// <summary>
/// Ответ приложения Пользователь
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string PasswordHash { get; init; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    public AccountStatuses AccountStatus { get; init; }

}