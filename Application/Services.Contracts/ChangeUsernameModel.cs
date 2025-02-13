namespace Services.Contracts;

/// <summary>
/// Модель смены имени пользователя
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="FirstName">Изменяемое имя пользователя</param>
/// <param name="LastName">Изменяемая фамилия</param>
public record ChangeUsernameModel(
    Guid Id,
    string FirstName,
    string LastName) : BaseModel<Guid>(Id);