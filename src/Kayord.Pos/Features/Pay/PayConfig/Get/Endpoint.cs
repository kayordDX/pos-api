using Kayord.Pos.Data;
namespace Kayord.Pos.Features.Pay.PayConfig.Get
{
    public class Endpoint : Endpoint<Request, Entities.HaloConfig>
    {
        private readonly AppDbContext _dbContext;
        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/pay/config/{outletId}");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var result = await Halo.GetHaloConfig(req.OutletId, _dbContext);
            await SendAsync(result);
        }
    }
}