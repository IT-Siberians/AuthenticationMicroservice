using Microsoft.AspNetCore.Http;
using Services.Abstractions;
using Services.Contracts;
using System.Security.Claims;

namespace AuthenticationDataManager.Cookies;

/// <summary>
/// Сервис для работы с данными аутентификации, хранящимися в cookies.
/// Предоставляет методы для построения объекта ClaimsPrincipal, извлечения информации о пользователе из cookies и получения схемы аутентификации.
/// </summary>
public class CookiesManagerService(
    IClaimsPrincipalBuilder<CookiesClaimsPrincipalBuilder> claimsPrincipalBuilder) : IAuthManagerService
{
    /// <summary>
    /// Строит объект ClaimsPrincipal на основе данных модели пользователя.
    /// </summary>
    /// <param name="model">Модель пользователя, из которой будут взяты данные для ClaimsPrincipal.</param>
    /// <returns>ClaimsPrincipal, содержащий информацию о пользователе.</returns>
    public ClaimsPrincipal BuildClaimsPrincipal(UserModel model)
    {
        return claimsPrincipalBuilder
            .AddIdentifier(model.Id.ToString())
            .AddUsername(model.Username)
            .AddEmail(model.Email)
            .AddAccountStatus(model.AccountStatus.ToString())
            .Build();
    }

    /// <summary>
    /// Получает схему аутентификации из cookies для текущего пользователя.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса, содержащий информацию об аутентификации.</param>
    /// <returns>Тип схемы аутентификации, или null, если пользователь не аутентифицирован.</returns>
    public string? GetAuthScheme(HttpContext context)
        => context.User.Identity.AuthenticationType;
}