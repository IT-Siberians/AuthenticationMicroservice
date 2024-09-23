﻿using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

/// <summary>
/// Сервис валидации изменений пользователей
/// </summary>
/// <param name="repository">Репозиторий пользователей</param>
/// <param name="hasher">Шифровальщик пароля</param>
public class UserValidationService(
    IUserRepository repository,
    IPasswordHasher hasher) : IUserValidationService
{
    /// <summary>
    /// Проверка свободно ли имя пользователя
    /// </summary>
    /// <param name="username">Проверяемое имя пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - имя пользователя свободно/ false - имя пользователя занято</returns>
    public async Task<bool> IsAvailableUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await repository.GetUserByUsernameAsync(username, cancellationToken) == null;
    }

    /// <summary>
    /// Проверка свободен ли Email
    /// </summary>
    /// <param name="email">Проверяемый Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <param name="id"></param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public async Task<bool> IsAvailableEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await repository.GetUserByEmailAsync(email, cancellationToken) == null;
    }

    /// <summary>
    /// Проверка свободен ли Email, через модель установки Email
    /// </summary>
    /// <param name="model">Модель установки Email</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - Email свободен/ false - Email занят</returns>
    public async Task<bool> IsAvailableEmailAsync(SetUserEmailModel model, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserByEmailAsync(model.NewEmail, cancellationToken);
        return user is null || user.Id == model.Id;
    }

    /// <summary>
    /// Валидация пароля
    /// </summary>
    /// <param name="validatePasswordModel">Модель валидации пароля</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - пароль верный/ false - некорректный пароль</returns>
    public async Task<bool> ValidatePasswordAsync(ValidatePasswordModel validatePasswordModel, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(validatePasswordModel.Id, cancellationToken);
        return user != null && hasher.VerifyHashedPassword(validatePasswordModel.Password, user.PasswordHash.Value);
    }

    /// <summary>
    /// Проверяет срок жизни ссылки
    /// </summary>
    /// <param name="expirationTime">Время создания ссылки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Возвращает true - срок не истек/ false - срок истек</returns>
    public Task<bool> IsLinkExpiredAsync(DateTime expirationTime, CancellationToken cancellationToken)
    {
        return Task.FromResult(expirationTime < DateTime.UtcNow);
    }
}