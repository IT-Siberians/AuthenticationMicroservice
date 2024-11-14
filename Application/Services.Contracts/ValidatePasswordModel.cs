namespace Services.Contracts;

/// <summary>
/// Модель валидации пароля
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="Password">Проверяемый пароль</param>
public record ValidatePasswordModel(
    Guid Id,
    string Password) : IBaseModel<Guid>;