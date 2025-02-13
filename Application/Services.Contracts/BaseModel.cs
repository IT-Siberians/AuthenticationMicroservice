namespace Services.Contracts;

/// <summary>
/// Базовый класс моделей
/// </summary>
/// <typeparam name="TId">Идентификатор модели</typeparam>
/// <param name="Id"> Идентификатор модели</param>
public abstract record BaseModel<TId>(TId Id)
    where TId : struct;