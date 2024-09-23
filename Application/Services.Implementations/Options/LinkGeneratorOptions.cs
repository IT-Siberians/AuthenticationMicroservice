namespace Services.Implementations.Options;

/// <summary>
/// Опции Сервиса генерации ссылок
/// </summary>
public class LinkGeneratorOptions
{
    /// <summary>
    /// Путь к домену
    /// </summary>
    public required string DomainPath { get; init; }

    /// <summary>
    /// Путь к контроллеру
    /// </summary>
    public required string ControllerPath { get; init; }

    /// <summary>
    /// Путь к Action методу
    /// </summary>
    public required string ActionPath { get; init; }
}