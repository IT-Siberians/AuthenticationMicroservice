using System.Security.Claims;

namespace Services.Implementations;

/// <summary>
/// Строитель ClaimsPrincipal
/// </summary>
public class ClaimsPrincipalBuilder
{
    private readonly List<Claim> _claims;
    private readonly string _authenticationType;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ClaimsPrincipalBuilder"/>.
    /// </summary>
    /// <param name="authenticationType">Тип аутентификации, который используется для построения объекта <see cref="ClaimsPrincipal"/>.</param>
    /// <exception cref="ArgumentException">Возникает, если параметр <paramref name="authenticationType"/> является null, пустым или состоит только из пробелов.</exception>
    public ClaimsPrincipalBuilder(string authenticationType)
    {
        if (string.IsNullOrWhiteSpace(authenticationType))
            throw new ArgumentException("The parameter cannot be null, empty, or whitespaces", nameof(authenticationType));

        _authenticationType = authenticationType;
        _claims = [];
    }

    /// <summary>
    /// Добавляет статус аккаунта пользователя в сборку утверждений (claims) в качестве роли.
    /// </summary>
    /// <param name="id">Статус аккаунта пользователя, который будет добавлен как утверждение (claim) с типом <see cref="ClaimTypes.NameIdentifier"/>.</param>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр <paramref name="id"/> является null, пустым или состоит только из пробелов.</exception>
    /// <returns>Возвращает тот же экземпляр строителя для цепочки вызовов.</returns>
    public ClaimsPrincipalBuilder AddUserIdentifier(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("The parameter cannot be null, empty, or whitespaces", nameof(id));

        _claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
        return this;
    }

    /// <summary>
    /// Добавляет статус аккаунта пользователя в сборку утверждений (claims) в качестве роли.
    /// </summary>
    /// <param name="username">Статус аккаунта пользователя, который будет добавлен как утверждение (claim) с типом <see cref="ClaimTypes.Name"/>.</param>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр <paramref name="username"/> является null, пустым или состоит только из пробелов.</exception>
    /// <returns>Возвращает тот же экземпляр строителя для цепочки вызовов.</returns>
    public ClaimsPrincipalBuilder AddUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("The parameter cannot be null, empty, or whitespaces", nameof(username));

        _claims.Add(new Claim(ClaimTypes.Name, username));
        return this;
    }

    /// <summary>
    /// Добавляет статус аккаунта пользователя в сборку утверждений (claims) в качестве роли.
    /// </summary>
    /// <param name="email">Статус аккаунта пользователя, который будет добавлен как утверждение (claim) с типом <see cref="ClaimTypes.Email"/>.</param>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр <paramref name="email"/> является null, пустым или состоит только из пробелов.</exception>
    /// <returns>Возвращает тот же экземпляр строителя для цепочки вызовов.</returns>
    public ClaimsPrincipalBuilder AddEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("The parameter cannot be null, empty, or whitespaces", nameof(email));

        _claims.Add(new Claim(ClaimTypes.Email, email));
        return this;
    }

    /// <summary>
    /// Добавляет статус аккаунта пользователя в сборку утверждений (claims) в качестве роли.
    /// </summary>
    /// <param name="accountStatus">Статус аккаунта пользователя, который будет добавлен как утверждение (claim) с типом <see cref="ClaimTypes.Role"/>.</param>
    /// <exception cref="ArgumentException">Выбрасывается, если параметр <paramref name="accountStatus"/> является null, пустым или состоит только из пробелов.</exception>
    /// <returns>Возвращает тот же экземпляр строителя для цепочки вызовов.</returns>
    public ClaimsPrincipalBuilder AddAccountStatus(string accountStatus)
    {
        if (string.IsNullOrWhiteSpace(accountStatus))
            throw new ArgumentException("The parameter cannot be null, empty, or whitespaces", nameof(accountStatus));

        _claims.Add(new Claim(ClaimTypes.Role, accountStatus));
        return this;
    }

    /// <summary>
    /// Создает и возвращает экземпляр объекта <see cref="ClaimsPrincipal"/> на основе добавленных утверждений (claims).
    /// </summary>
    /// <returns>
    /// Экземпляр объекта <see cref="ClaimsPrincipal"/>, который содержит <see cref="ClaimsIdentity"/> с добавленными утверждениями.
    /// </returns>
    public ClaimsPrincipal Build()
    {
        var claimsIdentity = new ClaimsIdentity(_claims, _authenticationType);
        return new ClaimsPrincipal(claimsIdentity);
    }
}