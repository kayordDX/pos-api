using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Stock;

public static class StockManager
{
    public static async Task StockUpdate(List<int> orderItemIds, AppDbContext _dbContext, string userId, bool isReverse, CancellationToken ct)
    {
        List<int> stockCheck = [];
        foreach (int r in orderItemIds)
        {
            var orderInfo = await _dbContext.OrderItem
                .Where(x => x.OrderItemId == r)
                .Select(x => new { x.MenuItemId, x.MenuItem.DivisionId })
                .FirstOrDefaultAsync(ct);

            if (orderInfo == null) continue;

            List<StockPatch> stockToUpdate = new();

            // Menu Items
            var menuItemStock = await _dbContext.MenuItemStock
                .Where(x => x.MenuItemId == orderInfo.MenuItemId)
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity, Type = StockItemAuditType.MenuItem })
                .ToListAsync(ct);

            stockToUpdate.AddRange(menuItemStock);

            // Extras
            var extras = await _dbContext.Database.SqlQuery<StockPatch>($"""
                    select
                        e.stock_id,
                        e.quantity,
                        2 type
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
                        s.quantity,
                        3 type
                    from
                    order_item_option o
                    join option_stock s
                        on s.option_id = o.option_id
                    where o.order_item_id = {r};
                """).ToListAsync(ct);

            stockToUpdate.AddRange(options);

            // Bulk Items
            var menuItemBulkStock = await _dbContext.MenuItemBulkStock
                .Where(x => x.MenuItemId == orderInfo.MenuItemId)
                .Select(x => new StockPatch() { StockId = x.StockId, Quantity = x.Quantity, Type = StockItemAuditType.Bulk })
                .ToListAsync(ct);
            stockToUpdate.AddRange(menuItemBulkStock);

            foreach (var m in stockToUpdate)
            {
                var stockItem = await _dbContext.StockItem
                    .Where(x => x.StockId == m.StockId && x.DivisionId == orderInfo.DivisionId)
                    .FirstOrDefaultAsync(ct);

                if (stockItem == null)
                {
                    stockItem = new StockItem()
                    {
                        DivisionId = orderInfo.DivisionId ?? 0,
                        StockId = m.StockId,
                        Actual = 0,
                        Threshold = 0
                    };
                    await _dbContext.AddAsync(stockItem);
                    await _dbContext.SaveChangesAsync(ct);
                }

                bool isBulk = m.Type == StockItemAuditType.Bulk;
                int isReverseValue = isBulk || isReverse ? -1 : 1;

                decimal toActual = stockItem.Actual - m.Quantity * isReverseValue;
                if (toActual < 0)
                {
                    toActual = 0;
                }

                if (stockItem.Actual != toActual || toActual == 0)
                {
                    if (!stockCheck.Contains(stockItem.StockId))
                    {
                        stockCheck.Add(stockItem.StockId);
                    }
                    await _dbContext.StockItemAudit.AddAsync(new StockItemAudit()
                    {
                        OrderItemId = r,
                        FromActual = stockItem.Actual,
                        ToActual = toActual,
                        StockItemAuditTypeId = (int)m.Type,
                        StockItemId = stockItem.Id,
                        UserId = userId,
                        Updated = DateTime.Now,
                    });
                    if (toActual == 0)
                    {
                        Entities.MenuItem? menuItemUnavailable = await _dbContext.MenuItem.FirstOrDefaultAsync(x => x.MenuItemId == orderInfo.MenuItemId);
                        if (menuItemUnavailable != null)
                        {
                            menuItemUnavailable.IsAvailable = false;
                        }
                    }
                }

                stockItem.Actual = toActual;
            }
        }
        await _dbContext.SaveChangesAsync(ct);

        foreach (int s in stockCheck)
        {
            await StockAvailableCheck(s, _dbContext, ct);
        }
    }

    public static async Task StockAvailableCheck(int stockId, AppDbContext dbContext, CancellationToken ct)
    {
        await dbContext.Database.ExecuteSqlAsync($"""
            update menu_item m
            set is_available = a.is_available
            from (
                select 
                    mi.menu_item_id,
                    min(((si.actual - mmm.quantity) >= 0)::int)::bool is_available
                from menu_item mi
                join menu_item_stock mis
                on mi.menu_item_id = mis.menu_item_id
                join menu_item_stock mmm
                on mmm.menu_item_id = mis.menu_item_id
                join stock_item si
                on si.stock_id = mmm.stock_id
                and si.division_id = mi.division_id
                where mis.stock_id = {stockId}
                group by mi.menu_item_id
            ) a
            WHERE m.menu_item_id = a.menu_item_id
            AND m.is_available <> a.is_available
        """);

        // await dbContext.Database.ExecuteSqlAsync($"""
        //     update extra e
        //         set is_available = a.is_available
        //     from (
        //         select
        //             e.extra_id,
        //             min(((si.actual - ee.quantity) >= 0)::int)::bool is_available
        //         from extra e
        //         join extra_stock es
        //             on es.extra_id = e.extra_id
        //         join extra_stock ee
        //             on ee.extra_id = e.extra_id
        //         join menu_item_extra_group mep
        //             on mep.extra_group_id = e.extra_group_id
        //         join menu_item mi
        //             on mep.menu_item_id = mi.menu_item_id
        //         join stock_item si
        //             on si.stock_id = ee.stock_id
        //             and si.division_id = mi.division_id
        //         where ee.stock_id = {stockId}
        //         group by e.extra_id
        //     ) a
        //     WHERE e.extra_id = a.extra_id
        //     AND e.is_available <> a.is_available
        // """);

        // await dbContext.Database.ExecuteSqlAsync($"""
        //     update option o
        //         set is_available = a.is_available
        //     from (
        //         select
        //             o.option_id,
        //             min(((si.actual - oo.quantity) >= 0)::int)::bool is_available
        //         from option o
        //         join option_stock os
        //             on os.option_id = o.option_id
        //         join option_stock oo
        //             on oo.option_id = o.option_id
        //         join menu_item_option_group mop
        //             on mop.option_group_id = o.option_group_id
        //         join menu_item mi
        //             on mop.menu_item_id = mi.menu_item_id
        //         join stock_item si
        //             on si.stock_id = oo.stock_id
        //             and si.division_id = mi.division_id
        //         where oo.stock_id = {stockId}
        //         group by o.option_id
        //     ) a
        //     WHERE o.option_id = a.option_id
        //     AND o.is_available <> a.is_available
        // """);
    }

    public static async Task<bool> IsMenuItemAvailable(int menuItemId, AppDbContext dbContext, CancellationToken ct)
    {
        var isAvailable = await dbContext.MenuItem
            .Where(x => x.MenuItemId == menuItemId && x.IsAvailable == false)
            .FirstOrDefaultAsync(ct);
        return isAvailable == null;
    }

    public static async Task<bool> IsExtrasAvailable(List<int> extras, AppDbContext dbContext, CancellationToken ct)
    {
        var notAvailable = await dbContext.Extra
            .Where(x => extras.Contains(x.ExtraId) && x.IsAvailable == false)
            .ToListAsync(ct);
        return notAvailable.Count == 0;
    }

    public static async Task<bool> IsOptionsAvailable(List<int> options, AppDbContext dbContext, CancellationToken ct)
    {
        var notAvailable = await dbContext.Option
            .Where(x => options.Contains(x.OptionId) && x.IsAvailable == false)
            .ToListAsync(ct);
        return notAvailable.Count == 0;
    }
}