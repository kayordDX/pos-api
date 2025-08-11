using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock.Allocate.Item;

public static class AllocateItemUpdate
{
    public static async Task Status(int allocateId, AppDbContext dbContext, CancellationToken ct)
    {
        var items = await dbContext.StockAllocateItem
            .AsNoTracking()
            .Where(x => x.StockAllocateId == allocateId)
            .ToListAsync(ct);

        if (items.All(x => x.StockAllocateItemStatusId > 2))
        {
            var allocate = await dbContext.StockAllocate
                .Where(x => x.Id == allocateId)
                .FirstOrDefaultAsync(ct);

            if (allocate != null)
            {
                allocate.StockAllocateStatusId = 3;
                await dbContext.SaveChangesAsync(ct);
            }
        }
    }

    public static async Task<bool> StockCount(StockAllocateItem allocateItem, AppDbContext dbContext, CurrentUserService currentUserService, CancellationToken ct, int? toStockId)
    {
        // From Stock Item
        var fromItem = await dbContext.StockItem
            .Where(x => x.StockId == allocateItem.StockId && x.DivisionId == allocateItem.StockAllocate.FromDivisionId)
            .FirstOrDefaultAsync(ct);

        if (fromItem == null)
        {
            return false;
        }

        // Check if we have enough stock to allocate
        if (fromItem.Actual < allocateItem.Actual)
        {
            return false;
        }

        decimal fromItemPreviousActual = fromItem.Actual;
        fromItem.Actual = fromItem.Actual - allocateItem.Actual;

        await dbContext.StockItemAudit.AddAsync(new StockItemAudit()
        {
            FromActual = fromItemPreviousActual,
            ToActual = fromItem.Actual,
            StockItemAuditTypeId = 4,
            StockItemId = fromItem.Id,
            UserId = currentUserService.UserId ?? "",
            Updated = DateTime.Now,
            StockId = fromItem.StockId,
            StockAllocateId = allocateItem.StockAllocateId,
        });

        // To Stock Item
        var toItem = await dbContext.StockItem
            .Where(x => x.StockId == (toStockId ?? allocateItem.StockId) && x.DivisionId == allocateItem.StockAllocate.ToDivisionId)
            .FirstOrDefaultAsync(ct);

        if (toItem == null)
        {
            toItem = new StockItem()
            {
                StockId = toStockId ?? allocateItem.StockId,
                DivisionId = allocateItem.StockAllocate.ToDivisionId,
                Actual = 0,
                Threshold = 0,
            };
            await dbContext.AddAsync(toItem);
            await dbContext.SaveChangesAsync(ct);
        }

        decimal toItemPreviousActual = toItem.Actual;
        toItem.Actual = toItem.Actual + allocateItem.Actual;

        await dbContext.StockItemAudit.AddAsync(new StockItemAudit()
        {
            FromActual = toItemPreviousActual,
            ToActual = toItem.Actual,
            StockItemAuditTypeId = 4,
            StockItemId = toItem.Id,
            UserId = currentUserService.UserId ?? "",
            Updated = DateTime.Now,
            StockId = toItem.StockId,
            StockAllocateId = allocateItem.StockAllocateId,
        });

        await dbContext.SaveChangesAsync(ct);
        return true;
    }
}