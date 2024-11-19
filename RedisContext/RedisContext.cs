using StackExchange.Redis;

namespace Redis;

/// <summary>
/// Контекст Redis для работы с базой данных Redis.
/// Обеспечивает доступ к базе данных Redis с использованием соединения через IConnectionMultiplexer.
/// </summary>
public class RedisContext
{
    /// <summary>
    /// Экземпляр объекта Database, предоставляющий доступ к Redis базе данных.
    /// </summary>
    public IDatabase Database { get; }

    /// <summary>
    /// Конструктор для создания экземпляра RedisContext.
    /// Использует предоставленное соединение для инициализации базы данных.
    /// </summary>
    /// <param name="connection">Соединение с Redis сервером, реализующее интерфейс IConnectionMultiplexer.</param>
    public RedisContext(IConnectionMultiplexer connection)
    {
        Database = connection.GetDatabase();
    }
}
