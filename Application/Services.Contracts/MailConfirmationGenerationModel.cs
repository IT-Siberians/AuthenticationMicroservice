namespace Services.Contracts;

/// <summary>
/// Модель формирования подтверждения почты
/// </summary>
public class MailConfirmationGenerationModel : BaseModel<Guid>
{
    /// <summary>
    /// Новый Email пользователя
    /// </summary>
    public required string NewEmail { get; init; }
}