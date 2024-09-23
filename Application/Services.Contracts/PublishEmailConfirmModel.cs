namespace Services.Contracts;

/// <summary>
/// Модель подтверждения Email, для отправки в шину сообщений
/// </summary>
public class PublishEmailConfirmModel : BaseModel<Guid>
{
    /// <summary>
    /// Новый Email пользователя
    /// </summary>
    public required string NewEmail { get; init; }

    /// <summary>
    /// Ссылка, для подтверждения Email
    /// </summary>
    public required string Link { get; init; }
}