using Common.Helpers.Domain.Enums;

namespace WebApiAuthenticate.Responses;

/// <summary>
/// Информация о пользователе(ответ на запрос)
/// </summary>
/// <param name="Id">Идентификатор пользователя</param>
/// <param name="Username">Имя пользователя</param>
/// <param name="Email">Email пользователя</param>
/// <param name="AccountStatus">Статус аккаунта пользователя</param>
public record UserInfoResponse(
    Guid Id,
    string Username,
    string Email,
    AccountStatuses AccountStatus);