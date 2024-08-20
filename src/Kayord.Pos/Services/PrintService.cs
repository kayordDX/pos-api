using System.Text.Json;
using StackExchange.Redis;

namespace Kayord.Pos.Services;

public class PrintService
{
    private readonly RedisClient _redisClient;
    public PrintService(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }
    public async Task Print(List<byte[]> printInstructions, int outletId, int printerId)
    {
        var subscriber = await _redisClient.GetSubscriber();
        RedisChannel channel = new RedisChannel($"print:{outletId}:{printerId}", RedisChannel.PatternMode.Auto);
        string printInstructionsSerialized = JsonSerializer.Serialize(printInstructions);
        await subscriber.PublishAsync(channel, printInstructionsSerialized);
    }
}