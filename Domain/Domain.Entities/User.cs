using Common.Helpers.Domain.Enums;
using Domain.ValueObjects.ValueObjects;

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
        public LastName LastName { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public FirstName FirstName { get; set; }

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
        /// <param name="firstName">Имя пользователя.</param>
        /// <param name="lastName">Фамилия пользователя.</param>
        public User(Username username, PasswordHash passwordHash, Email email, FirstName firstName, LastName lastName)
        {
            Id = Guid.NewGuid();
            Username = username ?? throw new ArgumentNullException(nameof(username));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            AccountStatus = AccountStatuses.UnconfirmedAccount;
        }

        /// <summary>
        /// Изменение имени и фамилии пользователя.
        /// </summary>
        /// <param name="lastnameValue">Новая фамилия пользователя</param>
        /// <param name="firstNameValue">Новое имя пользователя.</param>
        public void ChangeFullname(FirstName firstName, LastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
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
