using Domain.Entities.Enums;
using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
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
    /// Регистрация гостя
    /// </summary>
    /// <param name="guest">Гость которого нужно зарегистрировать</param>
    public User(Guest guest)
    {
        Id = Guid.NewGuid();
        Username = guest.Username;
        PasswordHash = guest.PasswordHash;
        Email = guest.Email;
        AccountStatus = AccountStatuses.UnconfirmedAccount;
    }

    /// <summary>
    /// Изменение имени пользователя(никнейма)
    /// </summary>
    /// <param name="newUsername">Новое имя пользователя</param>
    public void ChangeUsername(string newUsername)
    {
        Username = new Username(newUsername);
    }

    /// <summary>
    /// Изменение хэшированного пароля
    /// </summary>
    /// <param name="newPasswordHash">Хэшированный пароль, на который заменяется</param>
    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = new PasswordHash(newPasswordHash);
    }

    /// <summary>
    /// Изменение электронной почты
    /// </summary>
    /// <param name="newEmail">Новая электронная почта</param>
    private void ChangeEmail(Email newEmail)
    {
        Email = newEmail;
    }

    /// <summary>
    /// Подтверждение и замена электронной почты
    /// </summary>
    /// <param name="newEmail">Новая электронная почта</param>
    public void ConfirmNewEmail(string newEmail)
    {
        ChangeEmail(new Email(newEmail));
        if (AccountStatus == AccountStatuses.UnconfirmedAccount)
        {
            AccountStatus = AccountStatuses.ConfirmedAccount;
        }
    }

    /// <summary>
    /// Удалить пользователя из проекта
    /// </summary>
    /// <returns>true - удален/false - не удален</returns>
    public bool DeleteHimself()
    {
        return true;
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