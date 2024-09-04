namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос смены почты
/// </summary>
public class NewEmailRequest
{
    /// <summary>
    /// Email на который создается запрос на смену
    /// </summary>
    public required string EmailValue { get; init; }
}