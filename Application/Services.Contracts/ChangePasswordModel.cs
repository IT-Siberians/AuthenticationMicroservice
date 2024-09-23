namespace Services.Contracts;

/// <summary>
/// Модель для смены пароля
/// </summary>
public class ChangePasswordModel : BaseModel<Guid>
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public required string NewPassword { get; init; }
}