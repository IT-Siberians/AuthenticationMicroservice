using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос смены почты
/// </summary>
public class ChangeEmailRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Email на который создается запрос на смену
    /// </summary>
    [Required]
    public string NewEmail { get; init; }
}