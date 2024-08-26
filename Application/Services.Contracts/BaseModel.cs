using System.ComponentModel.DataAnnotations;

namespace Services.Contracts;

/// <summary>
/// Базовый класс моделей
/// </summary>
/// <typeparam name="TId">Идентификатор модели</typeparam>
public abstract class BaseModel<TId> where TId : struct
{
    /// <summary>
    /// Идентификатор модели
    /// </summary>
    [Required]
    public TId Id { get; init; }
}