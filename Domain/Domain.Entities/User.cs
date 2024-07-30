using Domain.Entities.Enums;
using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;
public class User : IEntity<Guid>
{
    public Guid Id { get; }
    public Username Username { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public Email Email { get; private set; }
    public AccountStatuses AccountStatus { get; private set; }
    /// <summary>
    /// Конструктор для EF
    /// </summary>
    protected User()
    {

    }
    /// <summary>
    /// Зарегистрированный пользователь
    /// </summary>
    /// <param name="username">Value object Username - имя пользователя</param>
    /// <param name="passwordHash">Value object PasswordHash - хэшированный пароль</param>
    /// <param name="email">Value object Email - электронная почта</param>
    public User(Username username, PasswordHash passwordHash, Email email)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = passwordHash;
        Email = email;
        AccountStatus = AccountStatuses.UnconfirmedAccount;
    }
    /// <summary>
    /// Изменение имени пользователя(никнейма)
    /// </summary>
    /// <param name="newUsername">Value object Username - новое имя пользователя</param>
    public void ChangeUsername(Username newUsername)
    {
        Username = newUsername;
    }
    /// <summary>
    /// Изменение хэшированного пароля
    /// </summary>
    /// <param name="newPasswordHash">Value object PasswordHash - хэшированный пароль, на который заменяется</param>
    public void ChangePassword(PasswordHash newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
    /// <summary>
    /// Изменение электронной почты
    /// </summary>
    /// <param name="newEmail">Value object Email - новая электронная почта</param>
    private void ChangeEmail(Email newEmail)
    {
        Email = newEmail;
    }
    /// <summary>
    /// Подтверждение и замена электронной почты
    /// </summary>
    /// <param name="newEmail">Value object Email - новая электронная почта</param>
    public void ConfirmNewEmail(Email newEmail)
    {
        ChangeEmail(newEmail);
        if (AccountStatus == AccountStatuses.UnconfirmedAccount)
        {
            AccountStatus = AccountStatuses.ConfirmedAccount;
        }
    }
    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    /// <returns>true/false</returns>
    public bool SignIn()
    {
        return true;
    }
    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    /// <returns>true/false</returns>
    public bool SignOut()
    {
        return true;
    }
}