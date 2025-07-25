using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Pay.PayConfig.SetActive;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("pay/config/activate");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        if (req.IsEnabled == false)
        {
            var entity = await _dbContext.HaloConfig.FirstOrDefaultAsync(x => x.Id == req.Id);
            if (entity == null)
            {
                await Send.NotFoundAsync();
                return;
            }
            entity.IsEnabled = false;
        }
        else
        {
            var entities = await _dbContext.HaloConfig.Where(x => x.OutletId == req.OutletId).ToListAsync(ct);
            foreach (var item in entities)
            {
                item.IsEnabled = false;
                if (item.Id == req.Id)
                {
                    item.IsEnabled = true;
                }
            }
        }
        await _dbContext.SaveChangesAsync();
        await Send.NoContentAsync();
    }
}
