using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.List;

public class Endpoint : Endpoint<Request, List<PrinterDTO>>
{
    private readonly AppDbContext _dbContext;
    private readonly RedisClient _redisClient;

    public Endpoint(AppDbContext dbContext, RedisClient redisClient)
    {
        _dbContext = dbContext;
        _redisClient = redisClient;
    }

    public override void Configure()
    {
        Get("/printer/{outletId}");
    }

    public override async Task HandleAsync(Request r, CancellationToken ct)
    {
        var result = await _dbContext.Printer
            .Where(x => x.OutletId == r.OutletId)
            .OrderByDescending(x => x.IsEnabled)
                .ThenBy(x => x.PrinterName)
            .ProjectToDto()
            .ToListAsync();

        if (result.Count > 0)
        {
            try
            {
                // Get subscribed printers for outlet
                var db = await _redisClient.GetDatabaseAsync();
                var subscribedPrinters = await db.ExecuteAsync("PUBSUB", "CHANNELS", $"print:{r.OutletId}:*");

                List<string> printerChannels = ((StackExchange.Redis.RedisValue[]?)subscribedPrinters)?
                    .Select(x => x.ToString())?.ToList() ?? [];

                List<int> onlineDevices = [];
                foreach (var channel in printerChannels)
                {
                    var subCount = await db.ExecuteAsync("PUBSUB", "NUMSUB", channel);
                    var subCountList = ((StackExchange.Redis.RedisValue[]?)subCount)?.Select(x => x.ToString()).ToList() ?? [];
                    if (subCountList.Count > 1)
                    {
                        bool isSubConnected = subCountList[1] == "1";
                        if (isSubConnected)
                        {
                            int deviceId = int.Parse(channel.Replace($"print:{r.OutletId}:", "") ?? "0");
                            onlineDevices.Add(deviceId);
                        }
                    }
                }
                result.ForEach(x => x.IsConnected = onlineDevices.Contains(x.DeviceId));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
            }
        }

        await SendAsync(result);
    }
}