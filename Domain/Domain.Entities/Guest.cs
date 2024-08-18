using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Незарегистрированный пользователь
/// </summary>
/// <param name="username">Имя пользователя</param>
/// <param name="passwordHash">Хэшированный пароль</param>
/// <param name="email">Электронная почта</param>
public class Guest(string username, string passwordHash, string email)
{
    public PasswordHash PasswordHash => new(passwordHash);
    public Username Username => new(username);
    public Email Email => new(email);

    /// <summary>
    /// Зарегистрировать гостя
    /// </summary>
    /// <returns>Резульатат регистрации гостя - новый зарегистрированный пользователь.</returns>
    public User SignUp() => new(this);
}