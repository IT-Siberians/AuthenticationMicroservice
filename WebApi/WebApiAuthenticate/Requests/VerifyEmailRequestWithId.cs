namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос подтверждения Email
/// </summary>
public class VerifyEmailRequestWithId : BaseRequestWithId<Guid>
{
    /// <summary>
    /// Новый Email, который подтверждают
    /// </summary>
    public required string NewEmail { get; init; }

    /// <summary>
    /// Дата создания запроса
    /// </summary>
    public required DateTime CreatedDateTime { get; init; }
}