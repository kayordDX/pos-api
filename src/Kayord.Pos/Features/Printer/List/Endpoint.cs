using Kayord.Pos.Services;

namespace Kayord.Pos.Features.Printer.List
{
    public class Endpoint : Endpoint<Request, List<PrinterStatus>>
    {
        private readonly RedisClient _redisClient;

        public Endpoint(RedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public override void Configure()
        {
            Get("/printer/list/{outletId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request r, CancellationToken ct)
        {
            List<PrinterStatus> result = new();
            var keys = await _redisClient.GetKeys($"printer:{r.OutletId}:*");
            foreach (var key in keys)
            {
                var status = await _redisClient.GetObjectAsync<PrinterStatus>(key);
                if (status != null)
                {
                    result.Add(status);
                }
            }
            await SendAsync(result);
        }
    }
}