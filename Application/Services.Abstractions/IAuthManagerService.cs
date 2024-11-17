using Microsoft.AspNetCore.Http;
using Services.Contracts;
using System.Security.Claims;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс для управления аутентификацией в приложении.
/// Предоставляет методы для построения <see cref="ClaimsPrincipal"/> и получения схемы аутентификации.
/// </summary>
public interface IAuthManagerService
{
    /// <summary>
    /// Строит <see cref="ClaimsPrincipal"/> для указанной модели пользователя.
    /// <para>
    /// Используется для создания <see cref="ClaimsPrincipal"/>, который представляет аутентифицированного пользователя.
    /// </para>
    /// </summary>
    /// <param name="model">Модель пользователя, на основе которой будет построен <see cref="ClaimsPrincipal"/>.</param>
    /// <returns>Возвращает объект <see cref="ClaimsPrincipal"/>, содержащий информацию о пользователе.</returns>
    ClaimsPrincipal BuildClaimsPrincipal(UserModel model);

    /// <summary>
    /// Получает схему аутентификации для текущего контекста запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP запроса, в котором необходимо определить схему аутентификации.</param>
    /// <returns>Возвращает строку, представляющую схему аутентификации, или null, если схема не найдена.</returns>
    string? GetAuthScheme(HttpContext context);
}