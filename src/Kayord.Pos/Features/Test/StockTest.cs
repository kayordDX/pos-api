
using System.Diagnostics;
using Kayord.Pos.Data;
using Kayord.Pos.Features.Bill.EmailBill;
using Kayord.Pos.Features.Stock;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.Test;

public class Result
{
    public TimeSpan Time { get; set; }
}

public class StockTest : EndpointWithoutRequest<Result>
{
    private readonly AppDbContext _dbContext;

    public StockTest(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Get("/test/stock");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<int> orderItemIds = [304943, 304942, 304941];

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        await StockManager.StockUpdate(orderItemIds, _dbContext, "test", false, ct);
        stopwatch.Stop();

        Result result = new()
        {
            Time = stopwatch.Elapsed,
        };

        await Send.OkAsync(result);
    }
}