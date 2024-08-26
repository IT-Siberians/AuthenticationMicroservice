using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос подтверждения Email
/// </summary>
public class VerifyEmailRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Новый Email, который подтверждают
    /// </summary>
    [Required]
    public string NewEmail { get; init; }

    /// <summary>
    /// Дата создания запроса
    /// </summary>
    [Required]
    public DateTime CreatedDateTime { get; init; }
}