using System.Net;
using System.Text.Json;
using StackExchange.Redis;

namespace Kayord.Pos.Services;

public class RedisClient
{
    private static readonly TimeSpan defaultExpiry = TimeSpan.FromMinutes(10);
    private static Lazy<Task<ConnectionMultiplexer>> lazyConnection = new();
    private readonly IConfiguration _config;

    public RedisClient(IConfiguration config)
    {
        _config = config;
        lazyConnection = new Lazy<Task<ConnectionMultiplexer>>(() => ConnectAsync(_config.GetConnectionString("Redis") ?? "localhost:6379"));
    }

    private static async Task<ConnectionMultiplexer> ConnectAsync(string connectionString)
    {
        return await ConnectionMultiplexer.ConnectAsync(connectionString);
    }

    private async Task<IDatabase> GetDatabaseAsync()
    {
        var connection = await lazyConnection.Value;
        return connection.GetDatabase();
    }

    public async Task<IServer> GetServer()
    {
        var connection = await lazyConnection.Value;
        EndPoint endPoint = connection.GetEndPoints().First();
        return connection.GetServer(endPoint);
    }

    public async Task<ISubscriber> GetSubscriber()
    {
        var connection = await lazyConnection.Value;
        return connection.GetSubscriber();
    }

    private string SerializeObject<T>(T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    private T? DeserializeObject<T>(string serializedObj)
    {
        return JsonSerializer.Deserialize<T>(serializedObj);
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        return await SetValueAsync(key, value, defaultExpiry);
    }

    public async Task<bool> SetValueAsync(string key, string value, TimeSpan expiry)
    {
        var database = await GetDatabaseAsync();
        return await database.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetValueAsync(string key)
    {
        var database = await GetDatabaseAsync();
        return await database.StringGetAsync(key);
    }

    public async Task<IEnumerable<string>> GetKeys(string pattern)
    {
        var server = await GetServer();
        IEnumerable<string> keys = server.Keys(pattern: pattern).Select(x => x.ToString()) ?? [];
        return keys;
    }

    public async Task<bool> SetObjectAsync<T>(string key, T value)
    {
        var serializedValue = SerializeObject(value);
        return await SetValueAsync(key, serializedValue, defaultExpiry);
    }

    public async Task<bool> SetObjectAsync<T>(string key, T value, TimeSpan expiry)
    {
        var serializedValue = SerializeObject(value);
        return await SetValueAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetObjectAsync<T>(string key)
    {
        var serializedValue = await GetValueAsync(key);
        if (serializedValue != null)
            return DeserializeObject<T>(serializedValue);
        return default;
    }

    public async Task DeletePatternAsync(string pattern)
    {
        var keys = await GetKeys(pattern);
        foreach (var key in keys)
        {
            await DeleteKeyAsync(key);
        }
    }

    public async Task DeleteKeyAsync(string key)
    {
        var database = await GetDatabaseAsync();
        await database.KeyDeleteAsync(key);
    }
}