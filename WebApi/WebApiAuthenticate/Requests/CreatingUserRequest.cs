using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на создание пользователя
/// </summary>
public class CreatingUserRequest
{
    /// <summary>
    /// Имя пользователя(никнейм)
    /// </summary>
    [Required]
    public string Username { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public string Password { get; init; }

    /// <summary>
    /// Email
    /// </summary>
    [Required]
    public string Email { get; init; }
}