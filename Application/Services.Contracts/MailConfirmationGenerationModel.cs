namespace Services.Contracts;

/// <summary>
/// Модель формирования подтверждения почты
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="NewEmail">Новый Email пользователя</param>
public record MailConfirmationGenerationModel(
    Guid Id,
    string NewEmail) : BaseModel<Guid>(Id);