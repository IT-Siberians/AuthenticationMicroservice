using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;
/// <summary>
/// Незарегистрированный пользователь
/// </summary>
/// <param name="username">Value object Username - имя пользователя</param>
/// <param name="passwordHash">Value object PasswordHash - хэшированный пароль</param>
/// <param name="email">Value object Email - электронная почта</param>
public class Guest(string? username, string? passwordHash, string? email)
{
    public readonly Username Username = new(username);
    private readonly PasswordHash _passwordHash = new(passwordHash);
    public readonly Email Email = new(email);
    /// <summary>
    /// Зарегистрировать гостя
    /// </summary>
    /// <returns>Зарегистрированный пользователь</returns>
    public User SignUp()
    {
        return new User(Username, _passwordHash, Email);
    }
}