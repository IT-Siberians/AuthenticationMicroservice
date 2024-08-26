using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Модель создания пользователя
/// </summary>
public class CreateUserModel
{
    /// <summary>
    /// Имя создаваемого пользователя (никнейм)
    /// </summary>
    [Required]
    public string Username { get; set; }

    /// <summary>
    /// Пароль создаваемого пользователя
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Email создаваемого пользователя
    /// </summary>
    [Required]
    public string Email { get; set; }
}
