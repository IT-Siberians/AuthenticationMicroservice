namespace Services.Contracts;

/// <summary>
/// Маркерный интерфейс для модели с идентификатором
/// </summary>
/// <typeparam name="TId">Идентификатор модели</typeparam>
public interface IBaseModel<out TId>
    where TId : struct
{
    TId Id { get; }
}