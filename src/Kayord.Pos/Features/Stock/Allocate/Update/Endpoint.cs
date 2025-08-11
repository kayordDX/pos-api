using Kayord.Pos.Data;
using Kayord.Pos.Events;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;


namespace Kayord.Pos.Features.Stock.Allocate.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public Endpoint(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public override void Configure()
    {
        Put("/stock/allocate");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockAllocate
            .Where(x => x.Id == req.Id)
            .Include(x => x.Outlet)
            .Include(x => x.FromDivision)
            .Include(x => x.ToDivision)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            await Send.NotFoundAsync();
            return;
        }

        entity.StockAllocateStatusId = req.StockAllocateStatusId;

        // If status is in progress make all child items waiting
        if (entity.StockAllocateStatusId == 2)
        {
            var items = await _dbContext.StockAllocateItem.Where(x => x.StockAllocateId == entity.Id).ToListAsync(ct);

            foreach (var item in items)
            {
                item.StockAllocateItemStatusId = 2;
            }

            // Notify users about new items
            var users = items.Select(x => x.AssignedUserId).Distinct().ToList();
            if (users.Count > 0)
            {
                string title = $"New Allocation from {entity.Outlet.Name}";
                string body = $"You have received a new allocation for {entity.Outlet.Name}\nFrom: {entity.FromDivision.DivisionName}\nTo: {entity.ToDivision.DivisionName}";
                foreach (var user in users)
                {
                    await new NotificationEvent
                    {
                        UserId = user,
                        Title = title,
                        Body = body
                    }.PublishAsync(Mode.WaitForNone);
                }
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
