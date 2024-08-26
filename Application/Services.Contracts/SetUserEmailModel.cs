using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Модель верификации Email
/// </summary>
public class SetUserEmailModel : BaseModel<Guid>
{
    /// <summary>
    /// Подтверждаемый Email
    /// </summary>
    [Required]
    public string NewEmail { get; set; }
}
