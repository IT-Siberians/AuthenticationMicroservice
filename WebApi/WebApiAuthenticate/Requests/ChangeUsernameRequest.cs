using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену имени пользователя(никнейма)
/// </summary>
public class ChangeUsernameRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Имя пользователя(никнейм), на которое будет смена имени пользователя(никнейма)
    /// </summary>
    [Required]
    public string NewUsername { get; init; }
}