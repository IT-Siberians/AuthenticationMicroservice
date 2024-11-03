using Common.Helpers.Domain.Enums;

namespace Services.Contracts;

/// <summary>
/// Модель пользователя для чтения
/// </summary>
/// <param name="Id">Идентификатор модели</param>
/// <param name="Username">Имя пользователя(никнейм)</param>
/// <param name="Email">Email пользователя</param>
/// <param name="AccountStatus">Статус аккаунта</param>
public record UserModel(
    Guid Id,
    string Username,
    string Email,
    AccountStatuses AccountStatus) : BaseModel<Guid>(Id);