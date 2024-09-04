namespace Domain.Entities;

/// <summary>
/// Интерфейс сущности, которую можно пометить как удаленную
/// </summary>
public interface IDeletableSoftly
{
    /// <summary>
    /// Метка удаления
    /// </summary>
    public bool IsDeleted { get; }

    /// <summary>
    /// Удалить пользователя
    /// </summary>
    /// <returns>true - удален/false - не удален</returns>
    public bool MarkAsDeletedSoftly();
}