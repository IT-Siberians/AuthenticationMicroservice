namespace Services.Contracts;

/// <summary>
/// Модель верификации Email
/// </summary>
public class SetUserEmailModel : BaseModel<Guid>
{
    /// <summary>
    /// Подтверждаемый Email
    /// </summary>
    public required string NewEmail { get; set; }
}
