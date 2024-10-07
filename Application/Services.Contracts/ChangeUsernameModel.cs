namespace Services.Contracts;

/// <summary>
/// Модель смены имени пользователя(никнейма)
/// </summary>
public class ChangeUsernameModel : BaseModel<Guid>
{
    /// <summary>
    /// Новое имя пользователя
    /// </summary>
    public required string NewUsername { get; init; }
}