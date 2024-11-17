namespace WebApiAuthenticate.Helpers;

/// <summary>
/// Класс, содержащий константы, представляющие имена политик авторизации,
/// которые используются в атрибутах авторизации.
/// </summary>
public static class AttributePoliticsNameHelpers
{
    /// <summary>
    /// Имя политики авторизации, позволяющей доступ только владельцам ресурса.
    /// Используется в атрибутах для ограничения доступа к ресурсам на основе прав владельца.
    /// </summary>
    public const string OWNER_ONLY_POLITIC_NAME = "OwnerOnly";
}