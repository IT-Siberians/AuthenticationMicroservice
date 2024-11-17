namespace Services.Contracts;

/// <summary>
/// Модель для представления кода верификации для пользователя.
/// </summary>
/// <param name="Id">Идентификатор пользователя или объекта, для которого генерируется код верификации.</param>
/// <param name="VerificationCode">Сам код верификации.</param>
public record VerificationCodeModel(Guid Id, int VerificationCode) : BaseModel<Guid>(Id);