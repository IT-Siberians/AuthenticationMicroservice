using Microsoft.AspNetCore.Authentication.Cookies;
using Services.Abstractions;
using System.Security.Claims;

namespace AuthenticationDataManager.Cookies;

/// <summary>
/// Класс для построения объекта ClaimsPrincipal с использованием паттерна "строитель".
/// </summary>
public class CookiesClaimsPrincipalBuilder : IClaimsPrincipalBuilder<CookiesClaimsPrincipalBuilder>
{
    private const string SCHEME_NAME = CookieAuthenticationDefaults.AuthenticationScheme;
    private readonly List<Claim> _claims;

    /// <summary>
    /// Создает новый экземпляр ClaimsPrincipalBuilder.
    /// </summary>
    public CookiesClaimsPrincipalBuilder()
    {
        _claims = [];
    }

    /// <summary>
    /// Добавляет идентификатор пользователя в список утверждений.
    /// </summary>
    /// <param name="identifier">Идентификатор пользователя.</param>
    /// <returns>Текущий экземпляр ClaimsPrincipalBuilder.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если идентификатор пустой или null.</exception>
    public CookiesClaimsPrincipalBuilder AddIdentifier(string identifier)
    {
        if (string.IsNullOrWhiteSpace(identifier))
            throw new ArgumentException("Идентификатор не может быть пустым или null.", nameof(identifier));

        _claims.Add(new Claim(ClaimTypes.NameIdentifier, identifier));

        return this;
    }

    /// <summary>
    /// Добавляет имя пользователя в список утверждений.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>Текущий экземпляр ClaimsPrincipalBuilder.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если имя пользователя пустое или null.</exception>
    public CookiesClaimsPrincipalBuilder AddUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Имя пользователя не может быть пустым или null.", nameof(username));

        _claims.Add(new Claim(ClaimTypes.Name, username));
        return this;
    }

    /// <summary>
    /// Добавляет email пользователя в список утверждений.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Текущий экземпляр ClaimsPrincipalBuilder.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если email пустой или null.</exception>
    public CookiesClaimsPrincipalBuilder AddEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email не может быть пустым или null.", nameof(email));

        _claims.Add(new Claim(ClaimTypes.Email, email));
        return this;
    }

    /// <summary>
    /// Добавляет статус учетной записи (роль) в список утверждений.
    /// </summary>
    /// <param name="accountStatus">Статус учетной записи.</param>
    /// <returns>Текущий экземпляр ClaimsPrincipalBuilder.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если статус учетной записи пустой или null.</exception>
    public CookiesClaimsPrincipalBuilder AddAccountStatus(string accountStatus)
    {
        if (string.IsNullOrWhiteSpace(accountStatus))
            throw new ArgumentException("Статус учетной записи не может быть пустым или null.", nameof(accountStatus));

        _claims.Add(new Claim(ClaimTypes.Role, accountStatus));
        return this;
    }

    /// <summary>
    /// Создает объект ClaimsPrincipal на основе добавленных утверждений и схемы аутентификации.
    /// </summary>
    /// <returns>Экземпляр ClaimsPrincipal.</returns>
    public ClaimsPrincipal Build()
    {
        var claimsIdentity = new ClaimsIdentity(_claims, SCHEME_NAME);
        return new ClaimsPrincipal(claimsIdentity);
    }
}

