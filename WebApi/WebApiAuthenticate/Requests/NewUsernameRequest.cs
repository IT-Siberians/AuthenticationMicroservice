namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену имени пользователя(никнейма)
/// </summary>
/// <param name="UsernameValue">Имя пользователя(никнейм), на которое будет смена имени пользователя(никнейма)</param>
public record NewUsernameRequest(string UsernameValue);