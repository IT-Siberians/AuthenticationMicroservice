using Common.Helpers.Domain.Enums;

namespace WebApiAuthenticate.Responses;

/// <summary>
/// Интерфейс для структуры ответа
/// </summary>
public interface IUserInfoResponse<out TId>
    where TId : struct
{
    TId Id { get; }
    string Username { get; }
    string Email { get; }
    AccountStatuses AccountStatus { get; }
}