using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock;

public static class StockManager
{
    public static async Task StockUpdate(List<int> orderItemIds, AppDbContext _dbContext, string userId, CancellationToken ct)
    {
        foreach (int r in orderItemIds)
        {
            var menuItemId = await _dbContext.OrderItem
                .Where(x => x.OrderItemId == r)
                .Select(x => x.MenuItemId)
                .FirstOrDefaultAsync(ct);

            int divisionId = await _dbContext.MenuItem
                .Where(x => x.MenuItemId == menuItemId)
                .Select(x => x.DivisionId)
                .FirstOrDefaultAsync(ct) ?? 0;

            List<StockPatch> stockToUpdate = new();

            // Menu Items
            var menuItemStock = await _dbContext.MenuItemStock
                .Where(x => x.MenuItemId == menuItemId)
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity })
                .ToListAsync(ct);

            stockToUpdate.AddRange(menuItemStock);

            // Extras
            var extras = await _dbContext.Database.SqlQuery<StockPatch>($"""
                    select 
                        e.stock_id, 
                        e.quantity 
                    from order_item_extra o
                    join extra_stock e
                        on o.extra_id = e.extra_id
                    where o.order_item_id = {r};
                """).ToListAsync(ct);

            stockToUpdate.AddRange(extras);

            // Options
            var options = await _dbContext.Database.SqlQuery<StockPatch>($"""
                    select
                        s.stock_id,
                        s.quantity
                    from
                    order_item_option o
                    join option_stock s
                        on s.option_id = o.option_id
                    where o.order_item_id = {r};
                """).ToListAsync(ct);

            stockToUpdate.AddRange(options);

            foreach (var m in stockToUpdate)
            {
                var stockItem = await _dbContext.StockItem
                    .Where(x => x.StockId == m.StockId && x.DivisionId == divisionId)
                    .FirstOrDefaultAsync(ct);

                if (stockItem == null) continue;

                await _dbContext.StockItemAudit.AddAsync(new Entities.StockItemAudit()
                {
                    OrderItemId = r,
                    FromActual = stockItem.Actual,
                    ToActual = stockItem.Actual - m.Quantity,
                    StockItemAuditTypeId = 1,
                    StockItemId = stockItem.Id,
                    UserId = userId,
                    Updated = DateTime.Now,
                });
                stockItem.Actual -= m.Quantity;
            }
        }
        await _dbContext.SaveChangesAsync(ct);
    }
}