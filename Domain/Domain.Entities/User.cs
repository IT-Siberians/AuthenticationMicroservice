using Common.Helpers.Domain.Enums;
using Domain.ValueObjects.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User : IEntity<Guid>, IDeletableSoftly
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Имя пользователя(никнейм)
    /// </summary>
    public Username Username { get; private set; }

    /// <summary>
    /// Хэшированный пароль
    /// </summary>
    public PasswordHash PasswordHash { get; private set; }

    /// <summary>
    /// Email
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    public AccountStatuses AccountStatus { get; private set; }

    /// <summary>
    /// Маркер помечен ли пользователь как удаленный
    /// </summary>
    public bool IsDeleted {get; private set; }

    /// <summary>
    /// Маркер помечен ли пользователь как авторизованный
    /// </summary>
    public bool IsSignIn {get; private set; }

    /// <summary>
    /// Конструктор для EF
    /// </summary>
    protected User() 
    {

    }

    /// <summary>
    /// Конструктор для создания нового пользователя
    /// </summary>
    /// <param name="username">Базовый элемент имя пользователя(никнейм)</param>
    /// <param name="passwordHash">Базовый элемент Хэш пароля</param>
    /// <param name="email">Базовый элемент Email</param>
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
    /// <param name="username">Новое имя пользователя</param>
    public void ChangeUsername(string username)
    {
        Username = new Username(username);
    }

    /// <summary>
    /// Изменение хэшированного пароля
    /// </summary>
    /// <param name="passwordHash">Хэшированный пароль, на который заменяется</param>
    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = new PasswordHash(passwordHash);
    }

    /// <summary>
    /// Изменение электронной почты
    /// </summary>
    /// <param name="email">Новая электронная почта</param>
    private void ChangeEmail(Email email)
    {
        Email = email;
    }

    /// <summary>
    /// Подтверждение и замена электронной почты
    /// </summary>
    /// <param name="email">Новая электронная почта</param>
    public void ConfirmNewEmail(string email)
    {
        ChangeEmail(new Email(email));
        if (AccountStatus == AccountStatuses.UnconfirmedAccount)
        {
            AccountStatus = AccountStatuses.ConfirmedAccount;
        }
    }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <returns>true - удален/false - не удален</returns>
    public bool MarkAsDeletedSoftly() => IsDeleted = true;

    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    /// <returns>true/false</returns>
    public bool SignIn() => IsSignIn = true;


    /// <summary>
    /// Выход пользователя из системы
    /// </summary>
    /// <returns>true/false</returns>
    public bool SignOut()
    {
        IsSignIn = false;
        return !IsSignIn;
    }
}