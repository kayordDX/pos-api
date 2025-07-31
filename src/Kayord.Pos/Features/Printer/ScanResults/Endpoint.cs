using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Printer.ScanResults;

public class Endpoint : Endpoint<Request, Results>
{
    private readonly RedisClient _redisClient;

    public Endpoint(RedisClient redisClient)
    {
        _redisClient = redisClient;
    }

    public override void Configure()
    {
        Get("/printer/scan");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Results results = new();
        string? status = await _redisClient.GetValueAsync($"status-print:{req.OutletId}:{req.DeviceId}");
        string? output = await _redisClient.GetValueAsync($"result-print:{req.OutletId}:{req.DeviceId}");

        results.Status = status;
        results.Output = output;

        await Send.OkAsync(results);
    }
}