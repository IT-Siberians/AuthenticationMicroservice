namespace Domain.Entities.Enums;
public enum AccountStatuses
{
    /// <summary>
    /// Зарегистрированный аккаут, с НЕ подтвержденной электронной почтой
    /// </summary>
    UnconfirmedAccount = 0,
    /// <summary>
    /// Зарегистрированный аккаут, с подтвержденной электронной почтой
    /// </summary>
    ConfirmedAccount = 1,
    /// <summary>
    /// Зарегистрированный аккаунт, ограниченный админом
    /// </summary>
    RestrictedAccount = 2,
}
