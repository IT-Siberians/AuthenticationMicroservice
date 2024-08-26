using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Модель формирования подтверждения почты
/// </summary>
public class MailConfirmationGenerationModel : BaseModel<Guid>
{
    /// <summary>
    /// Новый Email пользователя
    /// </summary>
    [Required]
    public string NewEmail { get; set; }
}