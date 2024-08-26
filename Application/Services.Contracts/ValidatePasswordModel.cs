using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;


/// <summary>
/// Модель валидации пароля
/// </summary>
public class ValidatePasswordModel : BaseModel<Guid>
{
    /// <summary>
    /// Проверяемый пароль
    /// </summary>
    [Required]
    public string Password { get; set; }
}