namespace Services.Contracts;

/// <summary>
/// Модель верификации Email
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="NewEmail">Подтверждаемый Email</param>
public record SetUserEmailModel(
    Guid Id,
    string NewEmail) : IBaseModel<Guid>;
