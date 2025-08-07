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
    }

    public static async Task StockAvailableAllCheck(AppDbContext dbContext, CancellationToken ct)
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
            group by mi.menu_item_id
        ) a
        WHERE m.menu_item_id = a.menu_item_id
        AND m.is_available <> a.is_available
        """);
    }

    public static async Task<bool> IsMenuItemAvailable(int menuItemId, AppDbContext dbContext, CancellationToken ct)
    {
        var isAvailable = await dbContext.MenuItem
            .Where(x => x.MenuItemId == menuItemId && x.IsAvailable == false)
            .FirstOrDefaultAsync(ct);
        return isAvailable == null;
    }

    public class StockCheck
    {
        public bool IsAvailable { get; set; }
    }

    public static async Task<bool> IsExtrasAvailable(int orderItemId, int? divisionId, AppDbContext dbContext, CancellationToken ct)
    {
        if (divisionId == null)
        {
            return false;
        }

        var stockCheck = await dbContext.Database.SqlQuery<StockCheck>($"""
            select
                coalesce(min(((si.actual - es.quantity) >= 0)::int)::bool, true) is_available
            from order_item oi
            left join order_item_extra oie
                on oi.order_item_id = oie.order_item_id
            left join extra e
                on e.extra_id = oie.extra_id
            left join extra_stock es
                on es.extra_id = e.extra_id
            left join stock_item si
                on si.stock_id = es.stock_id
                and division_id = {divisionId}
            where oi.order_item_id = {orderItemId}
            group by oi.order_item_id
        """)
        .FirstOrDefaultAsync(ct);

        return stockCheck?.IsAvailable ?? false;
    }

    public static async Task<bool> IsOptionsAvailable(int orderItemId, int? divisionId, AppDbContext dbContext, CancellationToken ct)
    {
        if (divisionId == null)
        {
            return false;
        }

        var stockCheck = await dbContext.Database.SqlQuery<StockCheck>($"""
            select
                coalesce(min(((si.actual - os.quantity) >= 0)::int)::bool, true) is_available
            from order_item oi
            left join order_item_option oio
                on oi.order_item_id = oio.order_item_id
            left join option o
                on o.option_id = oio.option_id
            left join option_stock os
                on os.option_id = o.option_id
            left join stock_item si
                on si.stock_id = os.stock_id
                and division_id = {divisionId}
            where oi.order_item_id = {orderItemId}
        """).FirstOrDefaultAsync(ct);

        return stockCheck?.IsAvailable ?? false;
    }
}