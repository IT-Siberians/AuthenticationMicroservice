namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену имени пользователя(никнейма)
/// </summary>
public class ChangeUsernameRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Имя пользователя(никнейм), на которое будет смена имени пользователя(никнейма)
    /// </summary>
    public required string NewUsername { get; init; }
}