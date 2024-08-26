using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Модель смены имени пользователя(никнейма)
/// </summary>
public class ChangeUsernameModel : BaseModel<Guid>
{
    /// <summary>
    /// Новое имя пользователя
    /// </summary>
    [Required]
    public string NewUsername { get; set; }
}