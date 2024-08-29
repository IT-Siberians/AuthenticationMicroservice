using static Common.Helpers.EmailHelpers.EmailDomainMessages;

namespace Domain.ValueObjects.Exceptions.EmailExceptions;

/// <summary>
/// Исключительная ситуация создание пустого Email
/// </summary>
/// <param name="paramName">Название параметра, в котором произошло исключение</param>
internal class EmailEmptyException(string paramName)
    : ArgumentNullException(
        paramName: paramName,
        message: EMAIL_EMPTY_ERROR);
