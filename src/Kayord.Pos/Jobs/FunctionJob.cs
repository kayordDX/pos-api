using Kayord.Pos.Data;
using Kayord.Pos.Features.Stock;
using Microsoft.EntityFrameworkCore;
using TickerQ.Utilities.Base;

namespace Kayord.Pos.Jobs;

public class FunctionJob(AppDbContext dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    [TickerFunction("Stock Threshold")]
    public async Task StockThreshold(CancellationToken ct)
    {
        _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
        await _dbContext.Database.ExecuteSqlAsync($"SELECT update_stock_threshold();", ct);
    }

    [TickerFunction("Notification Log Cleanup")]
    public async Task NotificationLogCleanup(CancellationToken ct)
    {
        _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
        await _dbContext.Database.ExecuteSqlAsync($"delete from notification_log where date_inserted < NOW() - INTERVAL '1 months';", ct);
    }

    [TickerFunction("Stock Available Check All")]
    public async Task StockCheckAll(CancellationToken ct)
    {
        _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(1));
        await StockManager.StockAvailableAllCheck(_dbContext, ct);
    }

    [TickerFunction("Sql")]
    public async Task RawSql(TickerFunctionContext<string> tickerContext, CancellationToken ct)
    {
        _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
        var sql = tickerContext.Request;
        if (sql == null) return;
        await _dbContext.Database.ExecuteSqlRawAsync(sql, ct);
    }
}
