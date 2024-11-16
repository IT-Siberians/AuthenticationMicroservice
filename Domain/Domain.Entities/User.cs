using Common.Helpers.Domain.Enums;
using Domain.ValueObjects.ValueObjects;
using System.Globalization;

namespace Domain.Entities
{
    /// <summary>
    /// Сущность пользователя в системе.
    /// </summary>
    public class User : IEntity<Guid>, IDeletableSoftly
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Никнейм.
        /// </summary>
        public Username Username { get; private set; }

        /// <summary>
        /// Хэшированный пароль пользователя.
        /// </summary>
        public PasswordHash PasswordHash { get; private set; }

        /// <summary>
        /// Электронная почта пользователя.
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public Lastname Lastname { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public Firstname Firstname { get; set; }

        /// <summary>
        /// Статус аккаунта пользователя (например, подтвержден или не подтвержден).
        /// </summary>
        public AccountStatuses AccountStatus { get; private set; }

        /// <summary>
        /// Маркер, который показывает, был ли пользователь удален.
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Защищенный конструктор для использования в Entity Framework.
        /// </summary>
        protected User()
        {
        }

        /// <summary>
        /// Конструктор для создания нового пользователя.
        /// </summary>
        /// <param name="username">Никнейм.</param>
        /// <param name="passwordHash">Хэш пароля.</param>
        /// <param name="email">Электронная почта пользователя.</param>
        /// <param name="firstname">Имя пользователя.</param>
        /// <param name="lastname">Фамилия пользователя.</param>
        public User(Username username, PasswordHash passwordHash, Email email, Firstname firstname, Lastname lastname)
        {
            Id = Guid.NewGuid();
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Firstname = firstname ?? throw new ArgumentNullException(nameof(firstname));
            Lastname = lastname ?? throw new ArgumentNullException(nameof(lastname));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            AccountStatus = AccountStatuses.UnconfirmedAccount;
        }

        /// <summary>
        /// Изменение имени и фамилии пользователя.
        /// </summary>
        /// <param name="lastnameValue">Новая фамилия пользователя</param>
        /// <param name="firstNameValue">Новое имя пользователя.</param>
        public void ChangeFullname(Firstname firstname, Lastname lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        /// <summary>
        /// Изменение хэшированного пароля пользователя.
        /// </summary>
        /// <param name="passwordHash">Новый хэшированный пароль.</param>
        public void ChangePasswordHash(string passwordHash)
        {
            PasswordHash = new PasswordHash(passwordHash);
        }

        /// <summary>
        /// Изменение электронной почты пользователя (необходима для подтверждения).
        /// </summary>
        /// <param name="email">Новая электронная почта.</param>
        private void ChangeEmail(Email email)
        {
            Email = email;
        }

        /// <summary>
        /// Подтверждение новой электронной почты и смена статуса аккаунта.
        /// </summary>
        /// <param name="email">Новая электронная почта.</param>
        public void ConfirmNewEmail(Email email)
        {
            ChangeEmail(email);
            if (AccountStatus == AccountStatuses.UnconfirmedAccount)
            {
                AccountStatus = AccountStatuses.ConfirmedAccount;
            }
        }

        /// <summary>
        /// Помечает пользователя как удаленного (мягкое удаление).
        /// </summary>
        public void MarkAsDeletedSoftly() => IsDeleted = true;
    }
}
