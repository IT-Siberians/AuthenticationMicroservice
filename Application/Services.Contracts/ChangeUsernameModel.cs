namespace Services.Contracts;

/// <summary>
/// Модель смены имени пользователя(никнейма)
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="NewUsername">Новое имя пользователя(никнейм)</param>
public record ChangeUsernameModel(
    Guid Id,
    string NewUsername) : BaseModel<Guid>(Id);