using StackExchange.Redis;

namespace Redis;

public class RedisContext(IConnectionMultiplexer connection)
{
    public IDatabase Database { get; } = connection.GetDatabase();
}