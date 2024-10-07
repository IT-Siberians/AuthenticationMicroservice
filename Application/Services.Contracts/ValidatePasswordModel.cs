namespace Services.Contracts;


/// <summary>
/// Модель валидации пароля
/// </summary>
public class ValidatePasswordModel : BaseModel<Guid>
{
    /// <summary>
    /// Проверяемый пароль
    /// </summary>
    public required string Password { get; init; }
}