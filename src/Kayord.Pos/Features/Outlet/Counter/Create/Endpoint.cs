using Kayord.Pos.Data;
using Kayord.Pos.Entities;

namespace Kayord.Pos.Features.Outlet.Counter.Create;

public class Endpoint : Endpoint<Request, Guid>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/outlet/counter");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        OutletCounter entity = new OutletCounter()
        {
            DeviceName = req.DeviceName,
            OutletId = req.OutletId
        };

        await _dbContext.OutletCounter.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        await Send.OkAsync(entity.Id);
    }
}