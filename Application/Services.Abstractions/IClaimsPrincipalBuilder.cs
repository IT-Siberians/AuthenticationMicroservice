using System.Security.Claims;

namespace Services.Abstractions;

/// <summary>
/// Интерфейс для построения объекта <see cref="ClaimsPrincipal"/> с помощью цепочки вызовов методов.
/// <para>
/// Предоставляет методы для добавления различных утверждений (например, идентификатор, имя, email) в <see cref="ClaimsPrincipal"/>.
/// </para>
/// </summary>
/// <typeparam name="T">Тип, который возвращается после каждого вызова метода, для поддержки цепочки вызовов.</typeparam>
public interface IClaimsPrincipalBuilder<out T>
{
    /// <summary>
    /// Добавляет идентификатор пользователя в список утверждений.
    /// </summary>
    /// <param name="identifier">Идентификатор пользователя.</param>
    /// <returns>Текущий экземпляр <see cref="T"/> для продолжения цепочки вызовов.</returns>
    public T AddIdentifier(string identifier);

    /// <summary>
    /// Добавляет имя пользователя в список утверждений.
    /// </summary>
    /// <param name="username">Имя пользователя.</param>
    /// <returns>Текущий экземпляр <see cref="T"/> для продолжения цепочки вызовов.</returns>
    public T AddUsername(string username);

    /// <summary>
    /// Добавляет email пользователя в список утверждений.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <returns>Текущий экземпляр <see cref="T"/> для продолжения цепочки вызовов.</returns>
    public T AddEmail(string email);

    /// <summary>
    /// Добавляет статус учетной записи (роль) в список утверждений.
    /// </summary>
    /// <param name="accountStatus">Статус учетной записи (например, активен/неактивен).</param>
    /// <returns>Текущий экземпляр <see cref="T"/> для продолжения цепочки вызовов.</returns>
    public T AddAccountStatus(string accountStatus);

    /// <summary>
    /// Создает объект <see cref="ClaimsPrincipal"/> на основе добавленных утверждений.
    /// </summary>
    /// <returns>Экземпляр <see cref="ClaimsPrincipal"/>, содержащий все добавленные утверждения.</returns>
    public ClaimsPrincipal Build();
}