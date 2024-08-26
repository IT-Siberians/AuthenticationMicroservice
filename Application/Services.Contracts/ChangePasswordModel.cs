using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Модель для смены пароля
/// </summary>
public class ChangePasswordModel : BaseModel<Guid>
{
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required]
    public string NewPassword { get; set; }
}