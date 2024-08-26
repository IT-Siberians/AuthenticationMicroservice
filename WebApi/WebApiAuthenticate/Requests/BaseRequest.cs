using System.ComponentModel.DataAnnotations;

namespace WebApiAuthenticate.Requests;

/// <summary>
/// Базовая сущность запроса
/// </summary>
/// <typeparam name="TId">Идентификатор запроса</typeparam>
public class BaseRequest<TId> where TId : struct
{
    /// <summary>
    /// Идентификатор запроса
    /// </summary>
    [Required]
    public TId Id { get; init; }
}