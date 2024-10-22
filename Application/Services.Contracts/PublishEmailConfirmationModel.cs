namespace Services.Contracts;

/// <summary>
/// Модель подтверждения Email, для публикации
/// </summary>
public class PublishEmailConfirmationModel
{
    /// <summary>
    /// Email, куда отправить подтверждение
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Ссылка подтверждения Email
    /// </summary>
    public required string Link { get; init; }
}