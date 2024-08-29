using static Common.Helpers.UserHelpers.UserApplicationMessages;

namespace Services.Implementations.Exceptions;

/// <summary>
/// Исключительная ситуация пользователь не создался
/// </summary>
internal class UserNotCreatedException() : NullReferenceException(USER_NOT_CREATE_ERROR);