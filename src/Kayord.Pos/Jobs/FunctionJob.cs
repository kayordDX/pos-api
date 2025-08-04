using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;
using TickerQ.Utilities.Base;

namespace Kayord.Pos.Jobs;

public class FunctionJob
{
    private readonly AppDbContext _dbContext;

    public FunctionJob(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [TickerFunction("StockThreshold", "0 1 * * *")]
    public async Task StockThreshold(CancellationToken ct)
    {
        await _dbContext.Database.ExecuteSqlAsync($"SELECT update_stock_threshold();", ct);
    }

    [TickerFunction("NotificationLogCleanup")]
    public async Task NotificationLogCleanup(CancellationToken ct)
    {
        await _dbContext.Database.ExecuteSqlAsync($"delete from notification_log where date_inserted < NOW() - INTERVAL '1 months';", ct);
    }
}