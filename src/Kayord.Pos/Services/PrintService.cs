using System.Text.Json;
using Kayord.Pos.Features.Printer;
using StackExchange.Redis;

namespace Kayord.Pos.Services;

public class PrintService
{
    private readonly RedisClient _redisClient;
    public PrintService(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }
    public async Task Print(int outletId, int deviceId, PrintMessage printMessage)
    {
        var subscriber = await _redisClient.GetSubscriber();
        RedisChannel channel = new RedisChannel($"print:{outletId}:{deviceId}", RedisChannel.PatternMode.Auto);
        string printInstructionsSerialized = JsonSerializer.Serialize(printMessage);
        await subscriber.PublishAsync(channel, printInstructionsSerialized);
    }
}