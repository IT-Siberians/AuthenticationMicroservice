namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену имени пользователя(никнейма)
/// </summary>
public class NewUsernameRequest
{
    /// <summary>
    /// Имя пользователя(никнейм), на которое будет смена имени пользователя(никнейма)
    /// </summary>
    public required string UsernameValue { get; init; }
}