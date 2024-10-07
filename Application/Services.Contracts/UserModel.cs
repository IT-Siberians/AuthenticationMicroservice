using Common.Helpers.Domain.Enums;

namespace Services.Contracts;

/// <summary>
/// Модель пользователя для чтения
/// </summary>
public class UserModel : BaseModel<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    public required AccountStatuses AccountStatus { get; init; }

}