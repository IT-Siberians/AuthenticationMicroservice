using System.ComponentModel.DataAnnotations;
using Common.Helpers.Domain.Enums;

namespace WebApiAuthenticate.Responses;

/// <summary>
/// Ответ приложения Пользователь
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Required]
    public Guid Id {get; init; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string Username { get; init; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    [Required]
    public string PasswordHash { get; init; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required]
    public string Email { get; init; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    [Required]
    public AccountStatuses AccountStatus { get; init; }

}