namespace Services.Contracts;

/// <summary>
/// Модель для смены пароля
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="NewPassword">Новый пароль пользователя</param>
public record ChangePasswordModel(
    Guid Id,
    string NewPassword) : IBaseModel<Guid>;