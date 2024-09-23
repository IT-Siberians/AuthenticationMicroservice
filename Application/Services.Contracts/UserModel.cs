﻿using Domain.Entities.Domain.Enums;

namespace Services.Contracts;

/// <summary>
/// Модель пользователя для чтения
/// </summary>
public class UserModel : BaseModel<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// Хэш пароля пользователя
    /// </summary>
    public string PasswordHash { get; init; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Статус аккаунта пользователя
    /// </summary>
    public AccountStatuses AccountStatus { get; init; }

}