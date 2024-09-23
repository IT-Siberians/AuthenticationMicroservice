namespace Services.Contracts;

/// <summary>
/// Модель верификации Email
/// </summary>
public class SetUserEmailModel : BaseModel<Guid>
{
    /// <summary>
    /// Подтверждаемый Email
    /// </summary>
    public required string NewEmail { get; init; }

    /// <summary>
    /// Время создания ссылки
    /// </summary>
    public DateTime ExpirationDateTime { get; init; }
}
