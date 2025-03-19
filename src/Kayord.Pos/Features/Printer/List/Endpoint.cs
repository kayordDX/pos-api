using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.List
{
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
            // Get subscribed printers for outlet
            var db = await _redisClient.GetDatabaseAsync();
            var subscribedPrinters = await db.ExecuteAsync("PUBSUB", "CHANNELS", $"print:{r.OutletId}:*");
            List<int> connectedDevices = ((StackExchange.Redis.RedisValue[]?)subscribedPrinters)?
                .Select(x => int.Parse(x.ToString().Replace($"print:{r.OutletId}:", "") ?? "0"))?
                .ToList() ?? [];

            var result = await _dbContext.Printer
                .Where(x => x.OutletId == r.OutletId)
                .OrderByDescending(x => x.IsEnabled)
                    .ThenBy(x => x.PrinterName)
                .ProjectToDto()
                .ToListAsync();

            result.ForEach(x => x.IsConnected = connectedDevices.Contains(x.DeviceId));
            await SendAsync(result);
        }
    }
}