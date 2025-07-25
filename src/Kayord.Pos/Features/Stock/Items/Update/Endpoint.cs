using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Items.Update;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _currentUserService;

    public Endpoint(AppDbContext dbContext, CurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public override void Configure()
    {
        Put("/stock/items");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = await _dbContext.StockItem
            .Where(x => x.DivisionId == req.DivisionId && x.StockId == req.StockId)
            .FirstOrDefaultAsync(ct);

        if (entity == null)
        {
            entity = new StockItem()
            {
                DivisionId = req.DivisionId,
                StockId = req.StockId,
                Actual = req.Actual,
                Threshold = req.Threshold
            };
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        bool checkStock = false;
        if (entity.Actual != req.Actual)
        {
            checkStock = true;
            await _dbContext.StockItemAudit.AddAsync(new StockItemAudit()
            {
                FromActual = entity.Actual,
                ToActual = req.Actual,
                StockItemAuditTypeId = 6,
                StockItemId = entity.Id,
                UserId = _currentUserService.UserId ?? "",
                Updated = DateTime.Now,
            });
        }

        entity.Actual = req.Actual;
        entity.Threshold = req.Threshold;

        await _dbContext.SaveChangesAsync();

        if (checkStock)
        {
            await StockManager.StockAvailableCheck(entity.StockId, _dbContext, ct);
        }

        await Send.NoContentAsync();
    }
}
