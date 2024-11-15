using Common.Helpers.Domain.Enums;

namespace WebApiAuthenticate.Responses;

/// <summary>
/// Сущность пользователя(ответ на запрос), нужно только для проверки
/// </summary>
/// <param name="Id">Идентификатор пользователя</param>
/// <param name="Username">Никнейм пользователя</param>
/// <param name="FirstName">Имя пользователя</param>
/// <param name="LastName">Фамилия пользователя</param>
/// <param name="Email">Email пользователя</param>
/// <param name="AccountStatus">Статус аккаунта пользователя</param>
public record UserInfoResponse(
    Guid Id,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    AccountStatuses AccountStatus);