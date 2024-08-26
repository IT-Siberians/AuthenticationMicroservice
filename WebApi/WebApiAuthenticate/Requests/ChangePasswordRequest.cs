using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Запрос на смену пароля
/// </summary>
public class ChangePasswordRequest : BaseRequest<Guid>
{
    /// <summary>
    /// Проверяемый пароль
    /// </summary>
    [Required]
    public string OldPassword { get; init; }

    /// <summary>
    /// Новый пароль, на который происходит смена
    /// </summary>
    [Required]
    public string NewPassword { get; init; }
}