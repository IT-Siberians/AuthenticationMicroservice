namespace Services.Implementations.Exceptions;

/// <summary>
/// Исключительная ситуация пользователь не создался
/// </summary>
internal class UserNotCreatedException() : NullReferenceException(ErrorApplicationMessages.USER_NOT_CREATE_ERROR);