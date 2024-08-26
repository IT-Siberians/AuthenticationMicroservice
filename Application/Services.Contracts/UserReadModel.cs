using Domain.Entities.Enums;

namespace Services.Contracts;

/// <summary>
/// Модель пользователя для чтения
/// </summary>
public class UserReadModel : BaseModel<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    public AccountStatuses AccountStatus { get; set; }

}